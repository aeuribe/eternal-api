using eternal_api.Application.Families.Queries.DTOs;
using eternal_api.Application.Families.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Families.Queries.GetAllCategories
{
    public class GetAllFamiliesHandler : IRequestHandler<GetAllFamiliesQuery, IEnumerable<FamilyDto>>
    {
        private readonly IFamilyRepository _familyRepository;

        public GetAllFamiliesHandler(IFamilyRepository familyRepository) 
        {
            _familyRepository = familyRepository;
        }
        public async Task<IEnumerable<FamilyDto>> Handle(GetAllFamiliesQuery query, CancellationToken cancellationToken)
        {
            var categories = await _familyRepository.ListAsync();

            return categories.Select(c => new FamilyDto
            {
                Id = c.Id,
                Name = c.Name,
                FamilyCode = c.FamilyCode,

                Brand = c.Brand != null ? new BrandDto
                {
                    Id = c.Brand.Id,
                    Name = c.Brand.Name
                } : null,

                Class = c.Class != null ? new ClassDto
                {
                    Id = c.Class.Id,
                    Name = c.Class.Name
                } : null,
                isActive = c.isActive
            }).ToList();

        }
    }
}
