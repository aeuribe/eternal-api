using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.OrderDetails.Queries.GetOrderDetailById
{
    public class GetOrderDetailByIdQuery : IRequest<OrderDetailDto>
    {
        public Guid Id { set; get; }
    }
}
