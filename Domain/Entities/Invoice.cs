namespace eternal_api.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public string? POD { get; private set; }
        public string InvoiceNumber { get; private set; }

        // 1. CORRECCIÓN: PascalCase y private set para proteger la colección
        public ICollection<InvoiceDetail> invoiceDetails { get; private set; } = new List<InvoiceDetail>();

        protected Invoice() { }

        public Invoice(Guid id, Guid orderId, decimal total, string? pod, string invoiceNumber)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            OrderId = orderId;
            Total = total;
            POD = pod ?? string.Empty;
            InvoiceNumber = invoiceNumber;
        }

        public void AssignPOD(string? pod)
        {
            POD = pod;
        }

        public void DeletePOD()
        {
            POD = string.Empty;
        }

        // 2. NUEVO MÉTODO DDD: La factura controla cómo se agregan sus detalles
        public void AddDetail(Guid detailId, Guid productId, int quantity, decimal subtotal)
        {
            // Asumo que tu constructor de InvoiceDetail recibe (Id, InvoiceId, ProductId, Quantity, Subtotal)
            invoiceDetails.Add(new InvoiceDetail(detailId, this.Id, productId, quantity, subtotal));
        }
    }
}