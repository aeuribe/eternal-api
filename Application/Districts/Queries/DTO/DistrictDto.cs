using eternal_api.Application.Regions.Queries.DTO;

namespace eternal_api.Application.Districts.Queries.DTO
{
    public class DistrictDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid RegionId { get; set; }
        public RegionDto? Region { get; set; }
    }
}
