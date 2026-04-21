using eternal_api.Application.Brands.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQuery : IRequest<IEnumerable<BrandDto>>
    {
    }
}
