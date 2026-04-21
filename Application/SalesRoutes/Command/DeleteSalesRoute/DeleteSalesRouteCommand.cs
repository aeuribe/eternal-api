using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.DeleteSalesRoute
{
    public class DeleteSalesRouteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}