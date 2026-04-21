using eternal_api.Application.Stores.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQuery: IRequest<StoreDto> { public Guid Id { get; set; } }

}
