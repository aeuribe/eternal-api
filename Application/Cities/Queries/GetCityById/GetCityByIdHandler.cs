using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
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
            if (!await _cityRepository.ExistsAsync(query.Id))
            {
                throw new NotFoundException("City", query.Id);
            }

            var city = await _cityRepository.GetByIdAsync(query.Id);
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                State = city.State,
                Country = city.Country
            };
        }
    }

}
