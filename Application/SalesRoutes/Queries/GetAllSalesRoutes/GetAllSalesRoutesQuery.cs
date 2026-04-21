using eternal_api.Application.SalesRoutes.DTOs;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Queries.GetAllSalesRoutes
{
    // Retorna una lista de DTOs
    public class GetAllSalesRoutesQuery : IRequest<IEnumerable<SalesRouteDto>>
    {
    }
}