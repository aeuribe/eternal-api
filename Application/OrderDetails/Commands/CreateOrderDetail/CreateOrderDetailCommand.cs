using MediatR;

namespace eternal_api.Application.OrderDetails.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommand : IRequest<Guid>
    {
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
    }
}
