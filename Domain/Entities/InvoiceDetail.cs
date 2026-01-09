namespace eternal_api.Domain.Entities
{
    public class InvoiceDetail
    {
        // ID único de InvoiceDetail
        public Guid Id { get; set; } 

        // Cantidad facturada por producto
        public int Quantity { get; set; }

        // Subtotal facturado por producto
        public float Subtotal { get; set; }
        
        // Clave foránea de Invoice
        public Guid InvoiceId { get; set; }

        // Clave foránea de producto
        public Guid ProductId { get; set; }


        public InvoiceDetail(Guid invoiceId, Guid productId, int quantity, float subtotal)
        {
            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            ProductId = productId;
            Quantity = quantity;
            Subtotal = subtotal;
        }

        public void Update(Guid invoiceId, Guid productId, int quantity, float subtotal)
        {
            InvoiceId = invoiceId;
            ProductId = productId;
            Quantity = quantity;
            Subtotal = subtotal;
        }
    }
}
