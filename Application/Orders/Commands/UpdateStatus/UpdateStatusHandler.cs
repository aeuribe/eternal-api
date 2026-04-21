using eternal_api.Application.Orders.Interfaces;
using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.Orders.Commands.UpdateStatus
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, bool>
    {
        public readonly IOrderUnitOfWork _orderUnitOfWork;

        public UpdateStatusHandler(IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        public async Task<bool> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderUnitOfWork.OrderRepository.GetByIdAsync(command.OrderId);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with ID {command.OrderId} not found.");
            }

            // Máquina de estados: Ejecutamos el método según el Enum solicitado
            switch (command.NewStatus)
            {
                case OrderStatus.Invoiced:
                    order.MarkAsInvoiced();
                    break;
                case OrderStatus.Canceled:
                    order.MarkAsCanceled();
                    break;
                case OrderStatus.Created:
                    // Por lo general, no puedes devolver una orden a "Creada" si ya avanzó, 
                    // así que puedes ignorarlo o lanzar una excepción.
                    break;
                default:
                    throw new Exception($"El estado {(int)command.NewStatus} no es un estado válido para la orden.");
            }

            // Notificamos al tracker de EF Core que esta entidad cambió
            await _orderUnitOfWork.OrderRepository.UpdateStatus(order);

            // Guardamos el cambio en PostgreSQL
            await _orderUnitOfWork.CompleteAsync(cancellationToken);

            return true;
        }
    }
}

