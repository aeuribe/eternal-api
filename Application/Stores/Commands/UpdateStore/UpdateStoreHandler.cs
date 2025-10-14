
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreHandler
    {
        private readonly IStoreRepository _repo;

        public UpdateStoreHandler(IStoreRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateStoreCommand command)
        {
            var store = await _repo.GetByIdAsync(command.Id);
            if (store is null) return false;

            store.Name = command.Name;
            store.Address = command.Address;
            store.CityId = command.CityId;

            await _repo.UpdateAsync(store);
            return true;
        }
    }

}
