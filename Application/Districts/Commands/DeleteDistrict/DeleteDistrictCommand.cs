using MediatR;

namespace eternal_api.Application.Districts.Commands.DeleteDistrict
{
    public record DeleteDistrictCommand(Guid Id) : IRequest<(bool Succeeded, string ErrorMessage)>;
}
