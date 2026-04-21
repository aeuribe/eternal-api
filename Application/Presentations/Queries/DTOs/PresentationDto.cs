namespace eternal_api.Application.Presentations.Queries.DTOs
{
    public class PresentationDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string GenericCode { get; set; } = string.Empty;
        public decimal? Volume { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public FamilyDto Family { get; set; }
    }

    public class FamilyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FamilyCode { get; set; }
        public BrandDto Brand { get; set; }
        public ClassDto Class { get; set; }
        public bool isActive { get; set; }

    }

    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
