namespace eternal_api.Application.Orders.Queries.GetOrderDiscrepancies
{
    public class OrderDiscrepancyDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int OrderedQuantity { get; set; }
        public int InvoicedQuantity { get; set; }
        public int Difference => OrderedQuantity - InvoicedQuantity;
    }
}
