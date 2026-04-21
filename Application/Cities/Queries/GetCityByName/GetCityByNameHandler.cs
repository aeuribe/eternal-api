using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.Cities.Queries.DTOs;
using eternal_api.Domain.Enums; // <-- Necesario
using MediatR;

namespace eternal_api.Application.Cities.Queries.GetCityByName
{
    public class GetCityByNameHandler : IRequestHandler<GetCityByNameQuery, CityDto?>
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
                StatePrefix = city.State.GetPrefix(),
                StateFullName = city.State.GetFullName(),
                Country = "USA"
            };
        }
    }
}