namespace eternal_api.Domain.Entities
{
    public class Bill
    {
        // ID único de bill
        public Guid Id { get; set; }

        // Fecha de creación
        public DateTime CreatedAt { get; set; }
        
        // Monto total
        public decimal Total { get; set; }

        // Clave foránea de la orden asociada
        public Guid OrderId { get; set; }

        // Propiedad de navegación
        public Order Order { get; set; }
        
        // Lista de detalles de la factura
        public ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

        // Clave foranea de POD asociado
        public Guid? PodId { get; set; }

        // Propiedad de navegación
        public POD? POD { get; set; }

        public Bill(Guid orderId, decimal total)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            OrderId = orderId;
            Total = total;
        }

    }
}
