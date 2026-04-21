namespace eternal_api.Application.Invoices.Queries.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PodImageUrl { get; set; }
        public string InvoiceNumber { get; set; }

        public List<InvoiceDetailDto> Items { get; set; } = new();
        public class InvoiceDetailDto
        {
            public Guid InvoiceDetailId { get; set; } // <-- El ID del detalle generado por el Frontend
            public Guid InvoiceId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}
