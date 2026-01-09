// GetAllProductsQuery.cs
using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>> { }
}

