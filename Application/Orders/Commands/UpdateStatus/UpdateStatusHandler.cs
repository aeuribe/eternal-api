using MediatR;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Orders.Commands.UpdateStatus
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, bool>
    {
        public readonly IOrderRepository _orderRepository;

        public UpdateStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId);
            if (order == null)
            {
                // Podrías lanzar una excepción o registrar el evento
                throw new InvalidOperationException($"Order with ID {command.OrderId} not found.");
                
            }

            if (command.IsInvoiced)
            {
                order.UpdateStatusToInvoiced();
            }

            return true;
        }
    }
}
