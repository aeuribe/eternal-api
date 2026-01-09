namespace eternal_api.Application.Common.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { set; get; }
        public Guid SalespersonId { set; get; }
        public Guid StoreId { set; get; }
        public DateTime CreatedAt { set; get; }
        public string PO { set; get; }
        public string Status { set; get; }
    }
}
