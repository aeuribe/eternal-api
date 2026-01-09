using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.OrderDetails.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdHandler : IRequestHandler<GetOrderDetailByIdQuery, OrderDetailDto>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetOrderDetailByIdHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<OrderDetailDto> Handle(GetOrderDetailByIdQuery query, CancellationToken cancellationToken)
        {
            if (!await _orderDetailRepository.ExistsAsync(query.Id))
            {
                throw new NotFoundException("OrderDetail", query.Id);
            }

            var orderDetail = await _orderDetailRepository.GetByIdAsync(query.Id);
            return new OrderDetailDto
            {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity
            };
        }
    }
}
