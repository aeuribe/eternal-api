using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        public readonly IOrderRepository _orderRepository;
        public DeleteOrderHandler( IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.Id);
            if (order == null)
            {
                //Order not found
                return false;
            }
            await _orderRepository.DeleteAsync(order);
            return true;

        }
    }
}
