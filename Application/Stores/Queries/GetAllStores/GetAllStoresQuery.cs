
using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;
namespace eternal_api.Application.Stores.Queries.ListStores
{
    public class GetAllStoresQuery : IRequest<IEnumerable<StoreDto>> { }

}
