namespace eternal_api.Application.Prices.Queries.GetHistPriceByDate
{
    public class GetHistPriceByDateQuery
    {
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
