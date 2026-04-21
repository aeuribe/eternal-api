using eternal_api.Application.Areas.Queries.DTO;
using eternal_api.Application.Regions.Interfaces;
using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Regions.Queries.GetRegions
{
    public class GetRegionsHandler : IRequestHandler<GetRegionsQuery, IEnumerable<RegionDto>>
    {
        private readonly IRegionRepository _repository;
        public GetRegionsHandler(IRegionRepository repository) => _repository = repository;

        public async Task<IEnumerable<RegionDto>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = await _repository.GetAllAsync();
            return regions.Select(r => new RegionDto
            {
                Id = r.Id,
                Name = r.Name,
                AreaId = r.AreaId,
                Area = r.Area != null ? new AreaDto { Id = r.Area.Id, Name = r.Area.Name } : null
            });
        }
    }
}
