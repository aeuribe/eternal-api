namespace eternal_api.Application.Common.DTOs
{
    public class InvoiceDetailDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
    }
}
