using eternal_api.Application.Districts.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Districts.Queries.GetDistricts
{
    public record GetDistrictsQuery : IRequest<IEnumerable<DistrictDto>>;
}
