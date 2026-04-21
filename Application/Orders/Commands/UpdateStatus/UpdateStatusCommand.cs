using eternal_api.Domain.Enums;
using MediatR;
namespace eternal_api.Application.Orders.Commands.UpdateStatus
{
    public class UpdateStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; } 
    }
}
