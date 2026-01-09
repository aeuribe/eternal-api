using MediatR;

namespace eternal_api.Application.OrderDetails.Commands.UpdateOrderDetail
{
    public class UpdateOrderDetailCommand : IRequest<bool>
    {
        public Guid Id { set; get; }
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
    }
}
