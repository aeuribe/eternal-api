using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoreByName
{
    public class GetStoreByNameQuery : IRequest<StoreDto> { public string Name { get; set; } = string.Empty; }

}
