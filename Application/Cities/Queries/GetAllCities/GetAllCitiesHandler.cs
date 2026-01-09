using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Cities.Queries.ListCities
{
    public class GetAllCitiesHandler: IRequestHandler<GetAllCitiesQuery, IEnumerable<CityDto>>
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
                State = c.State,
                Country = c.Country
            }).ToList();
        }
    }

}
