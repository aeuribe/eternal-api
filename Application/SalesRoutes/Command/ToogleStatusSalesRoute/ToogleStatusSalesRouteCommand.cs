using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.ToogleStatusSalesRoute
{
    public class ToogleStatusSalesRouteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
