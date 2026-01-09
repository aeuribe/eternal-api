using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;
namespace eternal_api.Application.Stores.Queries.ListStores
{
    public class GetAllStoresQuery : IRequest<IEnumerable<StoreDto>> { }

}
