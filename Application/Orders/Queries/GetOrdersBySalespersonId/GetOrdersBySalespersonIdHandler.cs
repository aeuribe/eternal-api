using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.Orders.Queries.DTOs;
using eternal_api.Application.Orders.Queries.GetOrdersBySalespersonId;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Orders.Queries.GetOrderBySalespersonId
{
    public class GetOrdersBySalespersonIdHandler : IRequestHandler<GetOrdersBySalespersonIdQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISalespersonValidationService _salespersonValidator;


        public GetOrdersBySalespersonIdHandler(IOrderRepository orderRepository, ISalespersonValidationService salespersonValidator)
        {
            _orderRepository = orderRepository;
            _salespersonValidator = salespersonValidator;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersBySalespersonIdQuery query, CancellationToken cancellationToken0)
        {
            if (!await _salespersonValidator.ExistsAsync(query.SalespersonId))
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
                Status = o.Status,
                PlanogramId = o.PlanogramId,

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
