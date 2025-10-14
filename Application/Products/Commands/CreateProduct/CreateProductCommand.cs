namespace eternal_api.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public bool isActive { get; set; } = true;
    }
}