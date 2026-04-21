using eternal_api.Domain.Enums;

namespace eternal_api.Application.Orders.Queries.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { set; get; }
        public Guid SalespersonId { set; get; }
        public Guid StoreId { set; get; }
        public DateTime CreatedAt { set; get; }
        public string PO { set; get; }
        public OrderStatus Status { set; get; }
        public Guid PlanogramId { set; get; }

        public List<OrderDetailDto> Items { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public Guid OrderDetailId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
