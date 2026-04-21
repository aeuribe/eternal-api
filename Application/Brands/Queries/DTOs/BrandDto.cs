namespace eternal_api.Application.Brands.Queries.DTOs
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool isActive { get; set; }
    }
}
