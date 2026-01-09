using eternal_api.Application.Common.DTOs;
using MediatR;
namespace eternal_api.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid OrderId { set; get; }
    }
}
