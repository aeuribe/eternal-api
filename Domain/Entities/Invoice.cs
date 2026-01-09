namespace eternal_api.Domain.Entities
{
    public class Invoice
    {
        // ID único de invoice
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
        public ICollection<InvoiceDetail> invoiceDetails { get; set; } = new List<InvoiceDetail>();

        // Propiedad de navegación
        public POD? POD { get; set; }

        //Se usa solo para EF Core
        public Guid? PodId { get; set; }

        public Invoice(Guid orderId, decimal total)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            OrderId = orderId;
            Total = total;
        }

        public void Update(decimal total, Guid orderId) 
        {
            Total = total;
            OrderId = orderId;
        }

        public void AssignPOD(string imageUrl)
        {
            if (POD != null)
                throw new InvalidOperationException($"Invoice {this.Id} already has a POD assigned");
            POD = new POD(imageUrl);
        }
    }
}