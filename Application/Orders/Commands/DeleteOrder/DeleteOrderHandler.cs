using eternal_api.Application.Orders.Interfaces;
using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        public readonly IOrderUnitOfWork _orderUnitOfWork;
        public DeleteOrderHandler( IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderUnitOfWork.OrderRepository.GetByIdAsync(command.Id);
            if (order == null) return false;

            // 2. Comparamos directamente con el Enum. ¡Cero strings mágicos!
            if (order.Status != OrderStatus.Canceled)
            {
                throw new InvalidOperationException(
                    "Solo se pueden eliminar físicamente pedidos que hayan sido cancelados previamente.");
            }

            await _orderUnitOfWork.OrderRepository.DeleteAsync(order);
            await _orderUnitOfWork.CompleteAsync(cancellationToken);

            return true;
        }
    }
}
