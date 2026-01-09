namespace eternal_api.Application.Common.DTOs
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
    }
}
