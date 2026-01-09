using MediatR;
namespace eternal_api.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid SalespersonId { get; set; }
        public Guid StoreId { get; set; }
        public string PO { get; set; }
    }
}
