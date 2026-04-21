using eternal_api.Application.Brands.Interfaces;
using eternal_api.Application.Brands.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        
        public GetAllBrandsHandler(IBrandRepository brandRepository) 
        {
            _brandRepository = brandRepository;
        }
        public async Task<IEnumerable<BrandDto>> Handle(GetAllBrandsQuery query, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.ListAsync();

            return brands.Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                isActive = b.isActive
            }).ToList();

        }
    }
}
