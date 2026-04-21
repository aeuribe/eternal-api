using eternal_api.Application.Stores.Interfaces;
using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdHandler : IRequestHandler<GetStoreByIdQuery, StoreDto?>
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
                StoreNumber = store.StoreNumber,
                ZoneNumber = store.ZoneNumber,
                ZipCode = store.ZipCode,
                Name = store.Name,
                Street = store.Street, // Actualizado de Address a Street
                IsActive = store.IsActive,
                HasPlanogram = store.HasPlanogram,
                CityId = store.CityId,
                DistrictId = store.DistrictId // Nuevo campo
            };
        }
    }
}