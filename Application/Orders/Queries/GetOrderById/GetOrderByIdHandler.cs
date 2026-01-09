using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eternal_api.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        public readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            if (!await _orderRepository.ExistsAsync(query.OrderId))
            {
                throw new NotFoundException("Order", query.OrderId);
            }
            var order = await _orderRepository.GetByIdAsync(query.OrderId);
            return new OrderDto
            {
                OrderId = order.Id,
                SalespersonId = order.SalespersonId,
                StoreId = order.StoreId,
                CreatedAt = order.CreatedAt,
                PO = order.PO,
                Status = order.Status
            };
            
        } 

    }
}
