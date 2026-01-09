using eternal_api.Application.Common.DTOs;
using MediatR;
namespace eternal_api.Application.Orders.Queries.GetOrderBySalespersonId
{
    public class GetOrdersBySalespersonIdQuery : IRequest<IEnumerable<OrderDto>>
    {
        public Guid SalespersonId { set; get; }
    }
}
