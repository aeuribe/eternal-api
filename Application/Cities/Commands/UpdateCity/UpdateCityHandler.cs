using eternal_api.Application.Cities.Interfaces;
using MediatR;

namespace eternal_api.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityHandler : IRequestHandler<UpdateCityCommand, bool>
    {
        private readonly ICityRepository _cityRepository;

        public UpdateCityHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<bool> Handle(UpdateCityCommand command, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(command.Id);
            if (city is null) return false;

            // Actualizamos la entidad con la nueva firma
            city.Update(command.Name, command.State);

            await _cityRepository.UpdateAsync(city);
            return true;
        }
    }
}