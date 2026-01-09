using MediatR;
using eternal_api.Application.Common.Interfaces;
namespace eternal_api.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        public readonly IOrderRepository _orderRepository;

        public UpdateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);
            order?.Update(command.SalespersonId, command.OrderId, command.PO);
            await _orderRepository.UpdateAsync(order);
            return true;
        }
    }
}
