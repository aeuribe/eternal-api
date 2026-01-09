using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Cities.Queries.GetCityByName
{
    public class GetCityByNameHandler: IRequestHandler<GetCityByNameQuery, CityDto>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityByNameHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<CityDto?> Handle(GetCityByNameQuery query, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByNameAsync(query.Name);
            return city is null ? null : new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                State = city.State,
                Country = city.Country
            };
        }
    }

}
