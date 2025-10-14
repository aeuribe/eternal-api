using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Stores.Queries.GetStoresByCity
{
    public class GetStoresByCityHandler
    {
        private readonly IStoreRepository _repo;
        public GetStoresByCityHandler(IStoreRepository repo) => _repo = repo;

        public async Task<List<StoreDto>> Handle(GetStoresByCityQuery query)
        {
            var stores = await _repo.GetByCityAsync(query.CityId);
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
