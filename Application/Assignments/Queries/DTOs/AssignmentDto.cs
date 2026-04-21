namespace eternal_api.Application.Assignments.Queries.DTOs
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public Guid SalesRouteId { get; set; }
        public Guid StoreId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
