using eternal_api.Application.Cities.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Cities.Queries.ListCities
{
    public class GetAllCitiesQuery: IRequest<IEnumerable<CityDto>> { }

}
