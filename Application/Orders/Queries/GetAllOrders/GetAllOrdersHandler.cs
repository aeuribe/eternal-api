using MediatR;
using eternal_api.Domain.Entities;
using eternal_api.Application.Orders.Queries.DTOs;
using eternal_api.Application.Orders.Interfaces;
namespace eternal_api.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            
            return orders.Select(o => new OrderDto 
            { 
                OrderId = o.Id,
                SalespersonId = o.SalespersonId,
                StoreId = o.StoreId,
                CreatedAt = o.CreatedAt,
                PO = o.PO,
                Status = o.Status,
                PlanogramId = o.PlanogramId,

                // Mapeo anidado: Transformamos cada OrderDetail en un OrderDetailDto
                // Usamos el operador '?.' por seguridad, en caso de que una orden venga sin detalles
                Items = o.orderDetails?.Select(d => new OrderDetailDto
                {
                    OrderDetailId = d.Id,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity
                }).ToList() ?? new List<OrderDetailDto>()
            }).ToList();
        }
    }
}
