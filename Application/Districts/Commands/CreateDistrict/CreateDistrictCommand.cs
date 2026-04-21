using MediatR;

namespace eternal_api.Application.Districts.Commands.CreateDistrict
{
    public record CreateDistrictCommand(Guid RegionId, string Name) : IRequest<Guid>;
}
