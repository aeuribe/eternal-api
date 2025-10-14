namespace eternal_api.Application.Common.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string SKU { get; set; }
        public bool isActive { get; set; }
    }
}