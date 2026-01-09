using MediatR;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        
        public CreateOrderHandler (IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
 
        public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {      
            var order = new Order(command.SalespersonId, command.StoreId, command.PO);
            await _orderRepository.AddAsync(order);
            return order.Id;       
        }

    }
}
