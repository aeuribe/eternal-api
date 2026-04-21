namespace eternal_api.Application.Planograms.Queries.DTOs
{
    public class PlanogramDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; } = true;
    }
}
