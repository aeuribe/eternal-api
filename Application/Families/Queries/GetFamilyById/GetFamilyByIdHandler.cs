using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Families.Queries.DTOs;
using eternal_api.Application.Families.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Families.Queries.GetCategoryById
{
    public class GetFamilyByIdHandler : IRequestHandler<GetFamilyByIdQuery, FamilyDto>
    {
        private readonly IFamilyRepository _familyRepository;

        public GetFamilyByIdHandler(IFamilyRepository familyRepository) 
        {
            _familyRepository = familyRepository;
        }
        public async Task<FamilyDto> Handle(GetFamilyByIdQuery query, CancellationToken cancellationToken)
        {
            // Verificar si existe
            if (!await _familyRepository.ExistsAsync(query.Id))
                throw new NotFoundException("Family", $"Family with id {query.Id} was not found.");

            // Obtener la entidad
            var family = await _familyRepository.GetByIdAsync(query.Id);

            return new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                FamilyCode = family.FamilyCode,
                isActive = family.isActive,
                Brand = family.Brand != null ? new BrandDto
                {
                    Id = family.Brand.Id,
                    Name = family.Brand.Name
                } : null,

                Class = family.Class != null ? new ClassDto
                {
                    Id = family.Class.Id,
                    Name = family.Class.Name
                } : null,
            };
        }
    }
}
