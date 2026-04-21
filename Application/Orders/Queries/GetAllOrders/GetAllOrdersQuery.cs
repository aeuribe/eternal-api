using eternal_api.Application.Orders.Queries.DTOs;
using MediatR;
namespace eternal_api.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}
