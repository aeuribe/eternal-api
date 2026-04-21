using MediatR;

namespace eternal_api.Application.Regions.Commands.UpdateRegion
{
    public record UpdateRegionCommand(Guid Id, Guid AreaId, string Name) : IRequest<bool>;
}
