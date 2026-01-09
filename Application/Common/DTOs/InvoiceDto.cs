namespace eternal_api.Application.Common.DTOs
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PodImageUrl { get; set; }

    }
}
