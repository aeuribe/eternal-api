using eternal_api.Application.Common.DTOs;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eternal_api.Application.Stores.Queries.ListStores
{
    public class ListStoresHandler
    {
        private readonly IStoreRepository _repo;
        public ListStoresHandler(IStoreRepository repo) => _repo = repo;

        public async Task<List<StoreDto>> Handle(ListStoresQuery query)
        {
            var stores = await _repo.ListAsync();
            return stores.Select(s => new StoreDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                IsActive = s.IsActive,
                CityId = s.CityId
            }).ToList();
        }
    }

}
