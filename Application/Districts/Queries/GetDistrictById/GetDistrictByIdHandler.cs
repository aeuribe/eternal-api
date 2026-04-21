using eternal_api.Application.Areas.Queries.DTO;
using eternal_api.Application.Districts.Interfaces;
using eternal_api.Application.Districts.Queries.DTO;
using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Districts.Queries.GetDistrictById
{
    public class GetDistrictByIdHandler : IRequestHandler<GetDistrictByIdQuery, DistrictDto?>
    {
        private readonly IDistrictRepository _repository;
        public GetDistrictByIdHandler(IDistrictRepository repository) => _repository = repository;

        public async Task<DistrictDto?> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
        {
            var district = await _repository.GetByIdAsync(request.Id);
            if (district == null) return null;

            return new DistrictDto
            {
                Id = district.Id,
                Name = district.Name,
                RegionId = district.RegionId,
                Region = district.Region != null ? new RegionDto
                {
                    Id = district.Region.Id,
                    Name = district.Region.Name,
                    AreaId = district.Region.AreaId,
                    Area = district.Region.Area != null ? new AreaDto
                    {
                        Id = district.Region.Area.Id,
                        Name = district.Region.Area.Name
                    } : null
                } : null
            };
        }
    }
}
