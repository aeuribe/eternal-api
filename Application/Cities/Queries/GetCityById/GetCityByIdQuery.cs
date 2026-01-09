using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Cities.Queries.GetCityByIdQuery
{
    public class GetCityByIdQuery: IRequest<CityDto> { public Guid Id { get; set; } }

}
