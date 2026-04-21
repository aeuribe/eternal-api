namespace eternal_api.Domain.Entities
{
    public class OrderDetail
    {
        // ID único del detalle de la orden
        public Guid Id { get; set; }
       // Cantidad de producto
        public int Quantity { get; set; }

        // Clave foranea de Order asociada
        public Guid OrderId  { get; set; }

        // Clave foranea de Producto asociado
        public Guid ProductId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        public OrderDetail() { }
        public OrderDetail(Guid id, int quantity, Guid orderId, Guid productId)
        {
            Id = id;
            Quantity = quantity;
            OrderId = orderId;
            ProductId = productId;
        }

        public void Update(int quantity, Guid productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }
    }


}
