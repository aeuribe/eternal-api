using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Cities.Commands.CreateCity
{
    public class CreateCityHandler
    {
        private readonly ICityRepository _cityRepository;

        public CreateCityHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Guid> Handle(CreateCityCommand command)
        {
            var city = new City
            {
                Name = command.Name,
                State = command.State,
                Country = command.Country
            };

            await _cityRepository.AddAsync(city);
            return city.Id;
        }
    }
}
