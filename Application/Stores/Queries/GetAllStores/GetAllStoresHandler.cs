using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations;

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
                Name = s.Name,
                Address = s.Address,
                IsActive = s.IsActive,
                CityId = s.CityId
            }).ToList();
        }
    }

}
