using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Cities.Queries.GetCityByName
{
    public class GetCityByNameHandler
    {
        private readonly ICityRepository _repo;

        public GetCityByNameHandler(ICityRepository repo)
        {
            _repo = repo;
        }

        public async Task<CityDto?> Handle(GetCityByNameQuery query)
        {
            var city = await _repo.GetByNameAsync(query.Name);
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
