using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.UpdateSalesRoute
{
    public class UpdateSalesRouteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Lo hacemos opcional porque si la ruta tiene ventas, el frontend no necesita enviarlo
        public Guid? CityId { get; set; }
    }
}
