using MediatR;
using eternal_api.Domain.Entities;
using eternal_api.Application.Orders.Interfaces;

namespace eternal_api.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly IPlanogramProvider _planogramProvider;

        public CreateOrderHandler(IOrderUnitOfWork unitOfWork, IPlanogramProvider planogramProvider)
        {
            _unitOfWork = unitOfWork;
            _planogramProvider = planogramProvider;
        }

        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // ==========================================
            // 1. IDEMPOTENCIA (Sincronización PWA offline)
            // ==========================================
            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(command.Id);
            if (existingOrder != null)
            {
                // Ya existe. Le decimos a Next.js "todo bien" para que lo borre de su cola local.
                return existingOrder.Id;
            }

            // ==========================================
            // 2. VALIDACIONES DE NEGOCIO
            // ==========================================
            var activePlanogram = await _planogramProvider.GetActivePlanogramAsync();

            if (activePlanogram == null)
            {
                throw new Exception("No se puede crear el pedido porque no hay un planograma activo en el sistema.");
            }

            // --- LÓGICA DE GENERACIÓN DEL PO ---
            var today = DateTime.UtcNow.Date;
            var ordersTodayCount = await _unitOfWork.OrderRepository.GetOrderCountByDateAsync(today);
            var nextSequence = ordersTodayCount + 1;
            var purchaseOrderString = $"PO-{today:yyyyMMdd}-{nextSequence:D4}";

            // Buscamos la asignación actual de la tienda para tomar la "fotografía" de la ruta
            var currentAssignment = await _unitOfWork.AssignmentRepository.GetByStoreIdAsync(command.StoreId);

            if (currentAssignment == null)
            {
                throw new Exception("Esta tienda no tiene un territorio comercial asignado actualmente. No se puede facturar.");
            }

            // ==========================================
            // 3. CONSTRUCCIÓN Y GUARDADO ATÓMICO (DDD Puro)
            // ==========================================
            var order = new Order(
                command.Id,
                command.SalespersonId,
                command.StoreId,
                activePlanogram.Id,
                purchaseOrderString,
                currentAssignment.SalesRouteId // <--- El RouteId va al final, como en tu constructor
            );

            // ¡AQUÍ ESTÁ LA MAGIA DEL AGGREGATE ROOT!
            // Llenamos la colección interna de la orden usando LINQ. 
            // Cero repositorios extra requeridos.
            foreach (var item in command.Items)
            {
                order.AddDetail(item.OrderDetailId, item.ProductId, item.Quantity);
            }

            // Añadimos solo la entidad raíz al tracker
            await _unitOfWork.OrderRepository.AddAsync(order);

            // Disparamos la transacción en PostgreSQL (Cabecera + Detalles juntos)
            await _unitOfWork.CompleteAsync(cancellationToken);

            return order.Id;
        }
    }
}