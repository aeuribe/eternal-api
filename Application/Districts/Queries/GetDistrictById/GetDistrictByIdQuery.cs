using eternal_api.Application.Districts.Queries.DTO;
using MediatR;

namespace eternal_api.Application.Districts.Queries.GetDistrictById
{
    public record GetDistrictByIdQuery(Guid Id) : IRequest<DistrictDto?>;
}
