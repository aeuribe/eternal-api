using MediatR;
namespace eternal_api.Application.Orders.Commands.UpdateStatus
{
    public class UpdateStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public bool IsInvoiced { get; set; }
    }
}
