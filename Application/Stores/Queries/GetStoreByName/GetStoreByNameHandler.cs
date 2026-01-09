using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoreByName
{
    public class GetStoreByNameHandler : IRequestHandler<GetStoreByNameQuery, StoreDto>
    {
        private readonly IStoreRepository _storeRepostory;
        public GetStoreByNameHandler(IStoreRepository storeRepostory)
        {
            _storeRepostory = storeRepostory;
        }
        public async Task<StoreDto?> Handle(GetStoreByNameQuery query, CancellationToken cancellationToken)
        {
            var store = await _storeRepostory.GetByNameAsync(query.Name);
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
