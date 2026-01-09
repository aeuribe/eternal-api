using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
namespace eternal_api.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        public readonly IOrderRepository _orderRepository;

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
                Status = o.Status
            }).ToList();
        }
    }
}
