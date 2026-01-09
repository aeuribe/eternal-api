using MediatR;

namespace eternal_api.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { set; get; }
    }
}
