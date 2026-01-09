using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.OrderDetails.Queries.GetAllOrderDetailsByOrderId
{
    public class GetAllOrderDetailsByOrderIdQuery : IRequest<IEnumerable<OrderDetailDto>>
    {
        public Guid OrderId { set; get; }
    }
}
