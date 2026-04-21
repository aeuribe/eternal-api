using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoresByDistrict
{
    public record GetStoresByDistrictQuery(Guid DistrictId) : IRequest<IEnumerable<StoreDto>>;
}
