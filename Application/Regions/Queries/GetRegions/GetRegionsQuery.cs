using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Regions.Queries.GetRegions
{
    public record GetRegionsQuery : IRequest<IEnumerable<RegionDto>>;
}
