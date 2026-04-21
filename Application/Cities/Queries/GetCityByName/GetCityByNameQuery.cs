using eternal_api.Application.Cities.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Cities.Queries.GetCityByName
{
    public class GetCityByNameQuery : IRequest<CityDto>
    {
        public string Name { get; set; } = string.Empty;
    }

}
