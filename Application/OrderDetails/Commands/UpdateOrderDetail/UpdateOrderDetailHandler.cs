using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.OrderDetails.Commands.UpdateOrderDetail
{
    public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand, bool>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public UpdateOrderDetailHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<bool> Handle(UpdateOrderDetailCommand command, CancellationToken cancellationToken)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(command.Id);
            if (orderDetail == null) return false;

            orderDetail.Update(command.Quantity, command.OrderId, command.ProductId);
            await _orderDetailRepository.UpdateAsync(orderDetail);
            return true;
        }
    }
}
