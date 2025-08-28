namespace eternal_api.Application.Common.DTOs
{
    public class BillDto
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
