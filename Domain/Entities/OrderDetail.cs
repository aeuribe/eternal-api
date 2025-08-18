namespace eternal_api.Domain.Entities
{
    public class OrderDetail
    {
        // ID único del detalle de la orden
        public int Id { get; set; }

       // Cantidad de producto
        public int Quantity { get; set; }

        // Clave foranea de Order asociada
        public int OrderId  { get; set; }

        // Clave foranea de Producto asociado
        public int ProductId { get; set; }

    }
}
