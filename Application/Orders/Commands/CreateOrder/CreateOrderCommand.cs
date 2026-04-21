using MediatR;
namespace eternal_api.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid SalespersonId { get; set; }
        public Guid StoreId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public Guid OrderDetailId { get; set; } // <-- El ID del detalle generado por el Frontend
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
