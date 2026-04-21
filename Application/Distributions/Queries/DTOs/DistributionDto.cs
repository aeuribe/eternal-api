namespace eternal_api.Application.Distributions.Queries.DTOs
{
    public class DistributionDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public Guid PlanogramId { get; set; }
        public Guid ProductId { get; set; }
    }
}
