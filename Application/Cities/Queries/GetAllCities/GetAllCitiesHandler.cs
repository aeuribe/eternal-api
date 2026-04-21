using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.Cities.Queries.DTOs;
using eternal_api.Domain.Enums; // <-- Necesario para los métodos de extensión
using MediatR;

namespace eternal_api.Application.Cities.Queries.ListCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<CityDto>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityDto>> Handle(GetAllCitiesQuery query, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.ListAsync();

            return cities.Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                StatePrefix = c.State.GetPrefix(),     // Usamos la extensión
                StateFullName = c.State.GetFullName(), // Usamos la extensión
                Country = "USA"
            }).ToList();
        }
    }
}