
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Commands.DesactivateStore
{
    public class DesactivateStoreHandler
    {
        private readonly IStoreRepository _repo;

        public DesactivateStoreHandler(IStoreRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DesactivateStoreCommand command)
        {
            var store = await _repo.GetByIdAsync(command.Id);
            if (store is null) return false;

            store.IsActive = false;
            await _repo.UpdateAsync(store);
            return true;
        }
    }

}
