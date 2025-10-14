using eternal_api.Domain.Entities;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Commands.CreateStore
{
    public class CreateStoreHandler
    {
        private readonly IStoreRepository _repo;

        public CreateStoreHandler(IStoreRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateStoreCommand command)
        {
            var store = new Store
            {
                Name = command.Name,
                Address = command.Address,
                IsActive = command.IsActive,
                CityId = command.CityId
            };

            await _repo.AddAsync(store);
            return store.Id;
        }
    }

}
