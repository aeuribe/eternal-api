using eternal_api.Application.SalesRoutes.DTOs;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Queries.GetSalesRouteById
{
    public class GetSalesRouteByIdQuery : IRequest<SalesRouteDto?>
    {
        public Guid Id { get; set; }
    }
}