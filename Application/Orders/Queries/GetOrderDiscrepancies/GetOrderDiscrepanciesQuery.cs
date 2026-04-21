using MediatR;

namespace eternal_api.Application.Orders.Queries.GetOrderDiscrepancies
{
    public class GetOrderDiscrepanciesQuery : IRequest<IEnumerable<OrderDiscrepancyDto>>
    {
        public Guid Id { get; set; }
    }
}
