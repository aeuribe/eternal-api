using eternal_api.Application.Areas.Queries.DTO;
using eternal_api.Application.Regions.Interfaces;
using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Regions.Queries.GetRegionById
{
    public class GetRegionByIdHandler : IRequestHandler<GetRegionByIdQuery, RegionDto?>
    {
        private readonly IRegionRepository _repository;

        public GetRegionByIdHandler(IRegionRepository repository) => _repository = repository;

        public async Task<RegionDto?> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var region = await _repository.GetByIdAsync(request.Id);

            if (region == null) return null;

            return new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                AreaId = region.AreaId,
                // Mapeamos el Área asociada gracias al Include() del repositorio
                Area = region.Area != null ? new AreaDto
                {
                    Id = region.Area.Id,
                    Name = region.Area.Name
                } : null
            };
        }
    }
}
