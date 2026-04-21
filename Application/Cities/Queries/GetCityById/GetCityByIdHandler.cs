using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.Cities.Queries.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Domain.Enums; // <-- Necesario
using MediatR;

namespace eternal_api.Application.Cities.Queries.GetCityByIdQuery
{
    public class GetCityByIdHandler : IRequestHandler<GetCityByIdQuery, CityDto>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityByIdHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<CityDto> Handle(GetCityByIdQuery query, CancellationToken cancellationToken)
        {
            // Optimización: Un solo viaje a la base de datos
            var city = await _cityRepository.GetByIdAsync(query.Id);

            if (city is null)
            {
                throw new NotFoundException("City", query.Id);
            }

            return new CityDto
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