using MediatR;

namespace eternal_api.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        // El ID de la orden que se va a actualizar
        public Guid Id { get; set; }

        // El ID de la tienda
        public Guid StoreId { get; set; }

        // La nueva lista de productos que reemplazará a la anterior
        public List<UpdateOrderItemDto> Items { get; set; } = new List<UpdateOrderItemDto>();
    }

    // Este DTO define qué datos necesitamos por cada producto en la orden
    public class UpdateOrderItemDto
    {
        public Guid OrderDetailId { get; set; } // El ID del OrderDetail
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}