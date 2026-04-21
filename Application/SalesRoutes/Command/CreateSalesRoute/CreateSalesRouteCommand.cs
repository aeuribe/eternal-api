using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.CreateSalesRoute
{
    public class CreateSalesRouteCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }

    }
}
