using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Cities.Commands.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, Guid>
    {
        private readonly ICityRepository _cityRepository;

        public CreateCityHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Guid> Handle(CreateCityCommand command, CancellationToken cancellationToken)
        {
            var city = new City(command.Name, command.State, command.Country);

            await _cityRepository.AddAsync(city);
            return city.Id;
        }
    }
}
