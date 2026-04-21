using MediatR;
using eternal_api.Application.Common.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using eternal_api.Application.Orders.Queries.DTOs;
using eternal_api.Application.Orders.Interfaces;

namespace eternal_api.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.OrderId);
            if (order == null)
            {
                throw new NotFoundException("Order", query.OrderId);
            }

            return new OrderDto
            {
                OrderId = order.Id,
                SalespersonId = order.SalespersonId,
                StoreId = order.StoreId,
                CreatedAt = order.CreatedAt,
                PO = order.PO,
                Status = order.Status,
                PlanogramId = order.PlanogramId,

                Items = order.orderDetails?.Select(d => new OrderDetailDto
                {
                    OrderDetailId = d.Id,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity
                }).ToList() ?? new List<OrderDetailDto>()
            };
            
        } 

    }
}
