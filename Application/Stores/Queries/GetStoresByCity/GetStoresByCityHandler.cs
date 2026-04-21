using eternal_api.Application.Stores.Interfaces;
using eternal_api.Application.Stores.Queries.DTOs;
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
                StoreNumber = s.StoreNumber,
                ZoneNumber = s.ZoneNumber,
                ZipCode = s.ZipCode,
                Name = s.Name,
                Street = s.Street, // Cambiado de Address a Street
                IsActive = s.IsActive,
                HasPlanogram = s.HasPlanogram,
                CityId = s.CityId,
                DistrictId = s.DistrictId // Agregado el DistrictId
            }).ToList();
        }
    }
}