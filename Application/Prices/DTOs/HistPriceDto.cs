namespace eternal_api.Application.Prices.DTOs
{
    public class HistPriceDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
