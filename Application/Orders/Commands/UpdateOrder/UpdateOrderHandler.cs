using eternal_api.Application.Orders.Interfaces;
using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        public readonly IOrderUnitOfWork _unitOfWork;

        public UpdateOrderHandler(IOrderUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Traemos la orden con sus detalles incluidos
            // Asegúrate de que tu GetByIdAsync tenga un .Include(o => o.OrderDetails)
            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id);

            if (existingOrder == null)
                throw new Exception($"La orden con ID {request.Id} no existe.");

            if (existingOrder.Status != OrderStatus.Created)
                throw new InvalidOperationException("Solo se pueden modificar pedidos que están en estado 'Creado'.");

            // 2. Actualizamos cabecera
            existingOrder.UpdateStore(request.StoreId);

            // 3. Preparamos la lista de tuplas para la entidad (DDD puro, sin filtrar DTOs al dominio)
            var incomingItems = request.Items.Select(i => (i.OrderDetailId, i.ProductId, i.Quantity)).ToList();

            // 4. Ejecutamos la magia diferencial
            existingOrder.UpdateDetails(incomingItems);

            // 5. Guardamos cambios
            await _unitOfWork.CompleteAsync(cancellationToken);

            return true;
        }
    }
}