using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdHandler
    {
        private readonly IStoreRepository _repo;
        public GetStoreByIdHandler(IStoreRepository repo) => _repo = repo;

        public async Task<StoreDto?> Handle(GetStoreByIdQuery query)
        {
            var store = await _repo.GetByIdAsync(query.Id);
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
