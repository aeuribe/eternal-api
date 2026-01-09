using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Products.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDto>>
    {
        public string Category { get; set; } = string.Empty;
    }
}
