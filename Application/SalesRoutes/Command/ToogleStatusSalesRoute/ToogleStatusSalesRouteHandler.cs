using eternal_api.Application.SalesRoutes.Interfaces;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.ToogleStatusSalesRoute
{
    public class ToogleStatusSalesRouteHandler : IRequestHandler<ToogleStatusSalesRouteCommand, bool>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;

        // Inyección de dependencias en el constructor
        public ToogleStatusSalesRouteHandler(ISalesRouteRepository salesRouteRepository)
        {
            _salesRouteRepository = salesRouteRepository;
        }

        public async Task<bool> Handle(ToogleStatusSalesRouteCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos la ruta en la base de datos
            var route = await _salesRouteRepository.GetByIdAsync(request.Id);

            if (route == null)
            {
                // Si no existe, podemos lanzar una excepción o simplemente retornar false
                throw new Exception("La ruta comercial especificada no existe.");
            }

            // 2. Invertimos el estado usando el método de tu entidad de Dominio
            // (Asegúrate de que tu entidad SalesRoute tenga este método, ej: public void Desactivate() { IsActive = !IsActive; })
            route.ToogleStatus();

            // 3. Llamamos al método optimizado para que Entity Framework solo haga UPDATE al campo IsActive
            await _salesRouteRepository.UpdateStatusAsync(route);
            await _salesRouteRepository.SaveChangesAsync();

            return true;
        }
    }
}