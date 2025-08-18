namespace eternal_api.Domain.Entities
{
    public class OrderDetail
    {
        // 
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId  { get; set; }
        public int ProductId { get; set; }

    }
}
