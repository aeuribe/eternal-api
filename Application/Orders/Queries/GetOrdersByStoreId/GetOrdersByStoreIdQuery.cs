using eternal_api.Application.Orders.Queries.DTOs;
using MediatR;
namespace eternal_api.Application.Orders.Queries.GetOrdersByStoreId
{
    public class GetOrdersByStoreIdQuery : IRequest<IEnumerable<OrderDto>>
    {
        public Guid StoreId { set; get; }
    }
}
