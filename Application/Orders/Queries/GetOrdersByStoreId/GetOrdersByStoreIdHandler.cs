using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.Orders.Queries.DTOs;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Orders.Queries.GetOrdersByStoreId
{
    public class GetOrdersByStoreIdHandler : IRequestHandler< GetOrdersByStoreIdQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreValidationService _storeValidator;

        public GetOrdersByStoreIdHandler(IOrderRepository orderRepository, IStoreValidationService storeValidator)
        {
            _orderRepository = orderRepository;
            _storeValidator = storeValidator;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByStoreIdQuery query, CancellationToken cancellationToken)
        {
            if(! await _storeValidator.ExistsAsync(query.StoreId))
            {
                throw new NotFoundException("Store", query.StoreId);
            }

            var orders = await _orderRepository.GetOrdersByStoreIdAsync(query.StoreId);

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
