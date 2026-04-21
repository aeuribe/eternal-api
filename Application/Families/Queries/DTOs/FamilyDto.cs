using eternal_api.Domain.Entities;

namespace eternal_api.Application.Families.Queries.DTOs
{
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
