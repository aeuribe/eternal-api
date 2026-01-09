namespace eternal_api.Application.Common.DTOs
{
    public class PlanogramDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; } = true;
    }
}
