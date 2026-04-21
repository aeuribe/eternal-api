using MediatR;

namespace eternal_api.Application.Districts.Commands.UpdateDistrict
{
    public record UpdateDistrictCommand(Guid Id, Guid RegionId, string Name) : IRequest<bool>;
}
