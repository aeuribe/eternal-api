using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Cities.Queries.GetCityByIdQuery
{
    public class GetCityByIdHandler
    {
        private readonly ICityRepository _repo;
        public GetCityByIdHandler(ICityRepository repo) => _repo = repo;

        public async Task<CityDto?> Handle(GetCityByIdQuery query)
        {
            var city = await _repo.GetByIdAsync(query.Id);
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
