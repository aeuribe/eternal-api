using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Orders.Interfaces;
using MediatR;

namespace eternal_api.Application.Orders.Queries.GetOrderDiscrepancies
{
    public class GetOrderDiscrepanciesHandler : IRequestHandler<GetOrderDiscrepanciesQuery, IEnumerable<OrderDiscrepancyDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInvoiceProvider _invoiceProvider;

        public GetOrderDiscrepanciesHandler(IOrderRepository orderRepository, IInvoiceProvider invoiceProvider)
        {
            _orderRepository = orderRepository;
            _invoiceProvider = invoiceProvider;
        }

        public async Task<IEnumerable<OrderDiscrepancyDto>> Handle(GetOrderDiscrepanciesQuery query, CancellationToken cancellationToken)
        {
            // 1. Traemos la Orden con sus detalles (y los nombres de los productos)
            var order = await _orderRepository.GetByIdAsync(query.Id);

            if (order == null || order.orderDetails == null)
            {
                return new List<OrderDiscrepancyDto>();
            }

            // 2. Traemos la Factura asociada a esta orden
            var invoice = await _invoiceProvider.GetInvoiceByOrderIdAsync(query.Id);

            // 3. EL MAPEO: Comparamos en memoria RAM dentro de la capa de Aplicación
            var discrepancies = order.orderDetails.Select(od => new OrderDiscrepancyDto
            {
                ProductId = od.ProductId,
                ProductName = od.Product?.Name ?? "N/A", // Evitamos nulos si el producto fue borrado
                OrderedQuantity = od.Quantity,

                // Buscamos si este producto específico se facturó, si no, es 0
                InvoicedQuantity = invoice?.invoiceDetails?
                    .FirstOrDefault(invDet => invDet.ProductId == od.ProductId)?.Quantity ?? 0
            }).ToList();

            return discrepancies;
        }
    }
}