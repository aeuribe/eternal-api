using eternal_api.Application.Areas.Queries.DTO;
using eternal_api.Application.Districts.Interfaces;
using eternal_api.Application.Districts.Queries.DTO;
using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Districts.Queries.GetDistricts
{
    public class GetDistrictsHandler : IRequestHandler<GetDistrictsQuery, IEnumerable<DistrictDto>>
    {
        private readonly IDistrictRepository _repository;
        public GetDistrictsHandler(IDistrictRepository repository) => _repository = repository;

        public async Task<IEnumerable<DistrictDto>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            var districts = await _repository.GetAllAsync();
            return districts.Select(d => new DistrictDto
            {
                Id = d.Id,
                Name = d.Name,
                RegionId = d.RegionId,
                Region = d.Region != null ? new RegionDto
                {
                    Id = d.Region.Id,
                    Name = d.Region.Name,
                    AreaId = d.Region.AreaId,
                    Area = d.Region.Area != null ? new AreaDto { Id = d.Region.Area.Id, Name = d.Region.Area.Name } : null
                } : null
            });
        }
    }
}
