using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly IStoreRepository _storeRepository;
        public GetStoreByIdHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<StoreDto?> Handle(GetStoreByIdQuery query, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.GetByIdAsync(query.Id);
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
