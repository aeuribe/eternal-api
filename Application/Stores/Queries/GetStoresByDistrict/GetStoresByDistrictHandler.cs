using eternal_api.Application.Stores.Interfaces;
using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoresByDistrict
{
    public class GetStoresByDistrictHandler : IRequestHandler<GetStoresByDistrictQuery, IEnumerable<StoreDto>>
    {
        private readonly IStoreRepository _storeRepository;

        public GetStoresByDistrictHandler(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<StoreDto>> Handle(GetStoresByDistrictQuery query, CancellationToken cancellationToken)
        {
            // Usamos el nuevo método que agregamos a tu interfaz
            var stores = await _storeRepository.GetByDistrictAsync(query.DistrictId);

            return stores.Select(s => new StoreDto
            {
                Id = s.Id,
                StoreNumber = s.StoreNumber,
                ZoneNumber = s.ZoneNumber,
                ZipCode = s.ZipCode,
                Name = s.Name,
                Street = s.Street,
                IsActive = s.IsActive,
                HasPlanogram = s.HasPlanogram,
                CityId = s.CityId,
                DistrictId = s.DistrictId
            }).ToList();
        }
    }
}