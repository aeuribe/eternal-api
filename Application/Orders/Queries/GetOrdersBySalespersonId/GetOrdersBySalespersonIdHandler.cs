using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Orders.Queries.GetOrderBySalespersonId
{
    public class GetOrdersBySalespersonIdHandler : IRequestHandler<GetOrdersBySalespersonIdQuery, IEnumerable<OrderDto>>
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IUserRepository _userRepository;

        public GetOrdersBySalespersonIdHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersBySalespersonIdQuery query, CancellationToken cancellationToken0)
        {
            if (!await _userRepository.ExistsAsync(query.SalespersonId))
            {
                throw new NotFoundException("Salesperson", query.SalespersonId);
            }

            var orders = await _orderRepository.GetOrdersByUserIdAsync(query.SalespersonId);

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
