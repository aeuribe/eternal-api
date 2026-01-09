using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoresByCity
{
    public class GetStoresByCityHandler : IRequestHandler<GetStoresByCityQuery, IEnumerable<StoreDto>>
    {
        private readonly IStoreRepository _storeRepository;
        public GetStoresByCityHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<IEnumerable<StoreDto>> Handle(GetStoresByCityQuery query, CancellationToken cancellationToken)
        {
            var stores = await _storeRepository.GetByCityAsync(query.CityId);
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
