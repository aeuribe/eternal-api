using eternal_api.Application.Areas.Queries.DTO;

namespace eternal_api.Application.Regions.Queries.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid AreaId { get; set; }
        public AreaDto? Area { get; set; }
    }
}
