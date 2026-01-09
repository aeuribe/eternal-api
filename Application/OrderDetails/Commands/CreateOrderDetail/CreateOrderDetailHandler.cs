using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.OrderDetails.Commands.CreateOrderDetail
{
    public class CreateOrderDetailHandler : IRequestHandler<CreateOrderDetailCommand, Guid>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public CreateOrderDetailHandler(IOrderDetailRepository orderDetailRepository) 
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Guid> Handle(CreateOrderDetailCommand command, CancellationToken cancellationToken)
        {
            var orderDetail = new OrderDetail(command.Quantity, command.OrderId, command.ProductId);
            await _orderDetailRepository.AddAsync(orderDetail);
            return orderDetail.Id;
        }
    }
}
