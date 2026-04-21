using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Application.Presentations.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Presentations.Queries.GetPresentationById
{
    public class GetPresentationByIdHandler : IRequestHandler<GetPresentationByIdQuery, PresentationDto?>
    {
        private readonly IPresentationRepository _presentationRepository;

        public GetPresentationByIdHandler(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        public async Task<PresentationDto?> Handle(GetPresentationByIdQuery query, CancellationToken cancellationToken)
        {
            // El repositorio ya trae el PriceHistory, Family, Brand y Class gracias a los Includes
            var presentation = await _presentationRepository.GetByIdAsync(query.Id);

            if (presentation is null) return null;

            return new PresentationDto
            {
                Id = presentation.Id,
                GenericCode = presentation.GenericCode,
                Volume = presentation.Volume,
                Unit = presentation.Unit,
                IsActive = presentation.IsActive,
                // Mapeamos la jerarquía completa
                Family = presentation.Family != null ? new FamilyDto
                {
                    Id = presentation.Family.Id,
                    Name = presentation.Family.Name,
                    FamilyCode = presentation.Family.FamilyCode, // Verifica que en tu entidad se llame FamilyCode
                    isActive = presentation.Family.isActive,

                    Brand = presentation.Family.Brand != null ? new BrandDto
                    {
                        Id = presentation.Family.Brand.Id,
                        Name = presentation.Family.Brand.Name
                    } : null,

                    Class = presentation.Family.Class != null ? new ClassDto
                    {
                        Id = presentation.Family.Class.Id,
                        Name = presentation.Family.Class.Name
                    } : null
                } : null
            };
        }
    }
}