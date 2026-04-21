using MediatR;

namespace eternal_api.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; }
        public bool isActive { get; set; } = true;
        public string ImageFileName { get; set; }
        public string ShortName { get; set; }
        public Guid PresentationId { get; set; }
        public string Sku { get; set; }
    }
}