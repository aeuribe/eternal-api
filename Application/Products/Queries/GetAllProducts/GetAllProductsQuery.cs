// GetAllProductsQuery.cs
using eternal_api.Application.Products.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>> { }
}

