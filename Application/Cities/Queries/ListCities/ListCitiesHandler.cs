using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Cities.Queries.ListCities
{
    public class ListCitiesHandler
    {
        private readonly ICityRepository _repo;

        public ListCitiesHandler(ICityRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CityDto>> Handle(ListCitiesQuery query)
        {
            var cities = await _repo.ListAsync();
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
