using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoresByCity
{
    public class GetStoresByCityQuery : IRequest<IEnumerable<StoreDto>> { public Guid CityId { get; set; } }

}
