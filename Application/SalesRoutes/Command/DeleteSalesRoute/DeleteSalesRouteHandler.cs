using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Application.Orders.Interfaces; // Vital para la validación cruzada
using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.DeleteSalesRoute
{
    public class DeleteSalesRouteHandler : IRequestHandler<DeleteSalesRouteCommand, bool>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteSalesRouteHandler(
            ISalesRouteRepository salesRouteRepository,
            IOrderRepository orderRepository)
        {
            _salesRouteRepository = salesRouteRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(DeleteSalesRouteCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos la ruta en PostgreSQL
            var route = await _salesRouteRepository.GetByIdAsync(request.Id);

            if (route == null)
            {
                // Si ya no existe, el trabajo ya está hecho (o es un error del front)
                throw new Exception("La ruta comercial especificada no existe.");
            }

            // 2. LA REGLA DE ORO: Verificamos si hay operaciones comerciales previas
            bool hasOrders = await _orderRepository.HasAnyOrderWithRouteAsync(request.Id);

            if (hasOrders)
            {
                // Si la ruta ya operó, bloqueamos el borrado físico (Hard Delete).
                // Tu middleware global de excepciones atrapará esto y le devolverá 
                // a Next.js un código HTTP 400 (Bad Request) o 409 (Conflict).
                throw new InvalidOperationException("No puedes eliminar esta ruta porque tiene un historial de ventas asociado. Por favor, utiliza la opción de desactivar (Toggle Status) para ocultarla.");
            }

            // 3. Si la ruta es completamente "virgen", permitimos el borrado físico
            await _salesRouteRepository.DeleteAsync(route);
            await _salesRouteRepository.SaveChangesAsync();

            return true;
        }
    }
}