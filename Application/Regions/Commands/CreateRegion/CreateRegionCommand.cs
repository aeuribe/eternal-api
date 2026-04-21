using MediatR;

namespace eternal_api.Application.Regions.Commands.CreateRegion
{
    public record CreateRegionCommand(Guid AreaId, string Name) : IRequest<Guid>;
}