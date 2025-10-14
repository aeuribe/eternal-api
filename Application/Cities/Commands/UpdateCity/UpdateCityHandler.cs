using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityHandler
    {
        private readonly ICityRepository _repo;

        public UpdateCityHandler(ICityRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateCityCommand command)
        {
            var city = await _repo.GetByIdAsync(command.Id);
            if (city is null) return false;

            city.Name = command.Name;
            city.State = command.State;
            city.Country = command.Country;

            await _repo.UpdateAsync(city);
            return true;
        }
    }

}
