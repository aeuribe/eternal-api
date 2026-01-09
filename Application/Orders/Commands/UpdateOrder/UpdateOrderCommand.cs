
using MediatR;
namespace eternal_api.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public Guid SalespersonId { get; set; }
        public Guid StoreId { get; set; }
        public string PO { get; set; }
    }
}
