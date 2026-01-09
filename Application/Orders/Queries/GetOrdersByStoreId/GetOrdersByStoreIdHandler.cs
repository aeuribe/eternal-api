using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Orders.Queries.GetOrdersByStoreId
{
    public class GetOrdersByStoreIdHandler : IRequestHandler< GetOrdersByStoreIdQuery, IEnumerable<OrderDto>>
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IStoreRepository _storeRepository;

        public GetOrdersByStoreIdHandler(IOrderRepository orderRepository, IStoreRepository storeRepository)
        {
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByStoreIdQuery query, CancellationToken cancellationToken)
        {
            if(! await _storeRepository.ExistsAsync(query.StoreId))
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
                Status = o.Status
            }).ToList();
        }
    }
}
