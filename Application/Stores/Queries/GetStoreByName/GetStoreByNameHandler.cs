using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Queries.GetStoreByName
{
    public class GetStoreByNameHandler
    {
        private readonly IStoreRepository _repo;
        public GetStoreByNameHandler(IStoreRepository repo) => _repo = repo;

        public async Task<StoreDto?> Handle(GetStoreByNameQuery query)
        {
            var store = await _repo.GetByNameAsync(query.Name);
            return store is null ? null : new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address,
                IsActive = store.IsActive,
                CityId = store.CityId
            };
        }
    }

}
