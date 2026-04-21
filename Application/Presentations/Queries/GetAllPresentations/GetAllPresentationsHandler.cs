using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Application.Presentations.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Presentations.Queries.GetAllPresentations
{
    public class GetAllPresentationsHandler : IRequestHandler<GetAllPresentationsQuery, IEnumerable<PresentationDto>>
    {
        private readonly IPresentationRepository _presentationRepository;

        // Eliminamos IHistPriceRepository porque usaremos los Includes de EF Core
        public GetAllPresentationsHandler(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        public async Task<IEnumerable<PresentationDto>> Handle(GetAllPresentationsQuery query, CancellationToken cancellationToken)
        {
            // La consulta masiva a la BD (ya trae PriceHistory, Family, Brand y Class gracias a tus Includes)
            var presentations = await _presentationRepository.ListAsync();

            return presentations.Select(p => new PresentationDto
            {
                Id = p.Id,
                GenericCode = p.GenericCode,
                Volume = p.Volume,
                Unit = p.Unit,
                IsActive = p.IsActive,


                // Mapeamos la jerarquía completa
                Family = p.Family != null ? new FamilyDto
                {
                    Id = p.Family.Id,
                    Name = p.Family.Name,
                    FamilyCode = p.Family.FamilyCode, // Asumo que en tu entidad se llama FamilyCode o Code
                    isActive = p.Family.isActive, // Asegúrate de que el casing coincida con tu entidad

                    Brand = p.Family.Brand != null ? new BrandDto
                    {
                        Id = p.Family.Brand.Id,
                        Name = p.Family.Brand.Name
                    } : null,

                    Class = p.Family.Class != null ? new ClassDto
                    {
                        Id = p.Family.Class.Id,
                        Name = p.Family.Class.Name
                    } : null
                } : null
            }).ToList();
        }
    }
}