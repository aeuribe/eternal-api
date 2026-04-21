using eternal_api.Application.Regions.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Regions.Queries.GetRegionById
{
    public record GetRegionByIdQuery(Guid Id) : IRequest<RegionDto?>;
}