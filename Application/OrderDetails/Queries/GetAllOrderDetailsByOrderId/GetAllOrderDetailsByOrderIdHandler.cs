using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.OrderDetails.Queries.GetAllOrderDetailsByOrderId
{
    public class GetAllOrderDetailsByOrderIdHandler : IRequestHandler<GetAllOrderDetailsByOrderIdQuery, IEnumerable<OrderDetailDto>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetAllOrderDetailsByOrderIdHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IEnumerable<OrderDetailDto>> Handle(GetAllOrderDetailsByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.GetAllByOrderIdAsync(query.OrderId);

            return orderDetails.Select(od => new OrderDetailDto
            {
                Id = od.Id,
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity
            }).ToList();
        }
    }
}
