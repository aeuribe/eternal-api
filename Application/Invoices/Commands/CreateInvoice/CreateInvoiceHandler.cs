using eternal_api.Application.Bills.Commands.CreateBill;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Invoices.Interfaces;
// Asegúrate de incluir la interfaz para acceder a las rutas
using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Invoices.Commands.CreateInvoice // Corregido el namespace en minúscula
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IInvoiceUnitOfWork _unitOfWork;
        private readonly IOrderValidationService _validateOrdersService;
        private readonly ISalesRouteRepository _salesRouteRepository; // NUEVO: Para sacar el "FL-01"

        public CreateInvoiceHandler(
            IInvoiceUnitOfWork invoiceUnitOfWorkRepository,
            IOrderValidationService validateOrdersService,
            ISalesRouteRepository salesRouteRepository)
        {
            _unitOfWork = invoiceUnitOfWorkRepository;
            _validateOrdersService = validateOrdersService;
            _salesRouteRepository = salesRouteRepository;
        }

        public async Task<Guid> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            // 1. IDEMPOTENCIA
            var existingInvoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(command.Id);
            if (existingInvoice != null)
            {
                return existingInvoice.Id;
            }

            // 2. VALIDACIÓN DE ORDEN
            var order = await _validateOrdersService.GetByIdAsync(command.OrderId);
            if (order == null)
            {
                throw new NotFoundException("Order", command.OrderId);
            }

            // ==========================================
            // 3. LÓGICA DEL CORRELATIVO LEGAL (FACTURA)
            // ==========================================

            // Buscamos la ruta de la orden para extraer el código (Ej: "FL-01")
            var route = await _salesRouteRepository.GetByIdAsync(order.SalesRouteId);
            string rawRouteCode = route != null ? route.Code.Replace("-", "") : "GEN"; // Resultado: "FL01" o "GEN"

            // Buscamos el último correlativo. 
            // OJO: Tu método viejo decía GetLastInvoiceNumberBySellerIdAsync. 
            // Ahora debería ser por Ruta (GetLastInvoiceNumberByRouteAsync) o general.
            string? lastInvoiceNumber = await _unitOfWork.InvoiceRepository.GetLastInvoiceNumberByRouteCodeAsync(rawRouteCode);

            int nextSequence = 1;
            if (!string.IsNullOrEmpty(lastInvoiceNumber))
            {
                var parts = lastInvoiceNumber.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out int lastNumber))
                {
                    nextSequence = lastNumber + 1;
                }
            }

            // Formato final: INV-FL01-00001
            string invoiceNumber = $"INV-{rawRouteCode}-{nextSequence:D5}";

            // ==========================================
            // 4. CREACIÓN Y PERSISTENCIA (DDD Puro)
            // ==========================================
            var invoice = new Invoice(command.Id, command.OrderId, command.Total, command.POD, invoiceNumber);

            // Usamos el Aggregate Root para agregar detalles limpiamente
            foreach (var item in command.Items)
            {
                invoice.AddDetail(item.InvoiceDetailId, item.ProductId, item.Quantity, item.Subtotal);
            }

            await _unitOfWork.InvoiceRepository.AddAsync(invoice);

            // Opcional pero recomendado: Actualizar el estado de la Orden a "Facturada"
            // order.MarkAsInvoiced();
            // await _validateOrdersService.UpdateAsync(order);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return invoice.Id;
        }
    }
}