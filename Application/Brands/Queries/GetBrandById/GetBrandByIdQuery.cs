using eternal_api.Application.Brands.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<BrandDto>
    {
        public Guid Id { get; set; }
    }
}
