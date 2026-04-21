using Amazon.S3.Model;
using MediatR;

namespace eternal_api.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand: IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; }
        public bool isActive { get; set; } = true;
        public string ImageFileName { get; set; }
        public string ShortName { get; set; }
        public Guid PresentationId { get; set; }
        public string Sku { get; set; }
    }
}