using eternal_api.Application.Stores.Interfaces;
using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.ListStores
{
    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, IEnumerable<StoreDto>>
    {
        private readonly IStoreRepository _storeRepository;

        public GetAllStoresHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<StoreDto>> Handle(GetAllStoresQuery query, CancellationToken cancellationToken)
        {
            var stores = await _storeRepository.ListAsync();

            return stores.Select(s => new StoreDto
            {
                Id = s.Id,
                StoreNumber = s.StoreNumber,
                ZoneNumber = s.ZoneNumber,
                ZipCode = s.ZipCode,
                Name = s.Name,
                Street = s.Street, // Mapeamos Street en lugar de Address
                IsActive = s.IsActive,
                HasPlanogram = s.HasPlanogram,
                CityId = s.CityId,
                DistrictId = s.DistrictId
            }).ToList();
        }
    }
}