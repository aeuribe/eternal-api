namespace eternal_api.Domain.Entities
{
    public class Order
    {
        // ID único de Order
        public Guid Id { get; set; }

        // Fecha de creación de la orden
        public DateTime CreateAt { get; set; }

        // Número de PO
        public string PO { get; set; }

        // Status de la orden que son "pending", "delivered", "canceled"
        public string Status { get; set; }

        // Clave foránea del vendedor
        public int SalespersonId { get; set; }

        // Clave foránea de la tienda
        public int StoreId { get; set; }

    }
}
