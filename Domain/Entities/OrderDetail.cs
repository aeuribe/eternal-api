namespace eternal_api.Domain.Entities
{
    public class OrderDetail
    {
        // ID único del detalle de la orden
        public Guid Id { get; set; } = Guid.NewGuid();

       // Cantidad de producto
        public int Quantity { get; set; }

        // Clave foranea de Order asociada
        public Guid OrderId  { get; set; }

        // Clave foranea de Producto asociado
        public Guid ProductId { get; set; }

        public OrderDetail(int quantity, Guid orderId, Guid productId)
        {
            Quantity = quantity;
            OrderId = orderId;
            ProductId = productId;
        }

        public void Update(int quantity, Guid orderId, Guid productId)
        {
            Quantity = quantity;
            OrderId = orderId;
            ProductId = productId;
        }
    }


}
