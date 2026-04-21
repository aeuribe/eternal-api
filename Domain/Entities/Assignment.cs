namespace eternal_api.Domain.Entities
{
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public DateTime AssignedAt { get; set; }

        public Guid SalesRouteId { get; set; }
        public SalesRoute SalesRoute { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public Assignment(Guid salesRouteId, Guid storeId) 
        {
            SalesRouteId = salesRouteId;
            StoreId = storeId;
            AssignedAt = DateTime.UtcNow;
        }

        public void Update(Guid salesRouteId, Guid storeId) 
        {
            SalesRouteId = salesRouteId;
            StoreId = storeId;
        }
    }
}
