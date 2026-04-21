using eternal_api.Application.Brands.Interfaces;
using eternal_api.Application.Brands.Queries.DTOs;
using eternal_api.Application.Common.Exceptions;
using MediatR;

namespace eternal_api.Application.Brands.Queries.GetBrandById
{
    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;

        public GetBrandByIdHandler(IBrandRepository brandRepository) 
        {
            _brandRepository = brandRepository;
        }
        public async Task<BrandDto> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
        {
            // Verificar si existe
            if (!await _brandRepository.ExistsAsync(query.Id))
                throw new NotFoundException("Brand",$"Brand with id {query.Id} was not found.");

            // Obtener la entidad
            var brand = await _brandRepository.GetByIdAsync(query.Id);

            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                isActive = brand.isActive
            };
        }
    }
}
