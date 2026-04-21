using MediatR;

namespace eternal_api.Application.Regions.Commands.DeleteRegion
{
    public record DeleteRegionCommand(Guid Id) : IRequest<(bool Succeeded, string ErrorMessage)>;
}