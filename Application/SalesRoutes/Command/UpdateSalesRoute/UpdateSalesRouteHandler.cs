using eternal_api.Application.Cities.Interfaces; // Necesario para buscar el prefijo
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.UpdateSalesRoute
{
    public class UpdateSalesRouteHandler : IRequestHandler<UpdateSalesRouteCommand, bool>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICityRepository _cityRepository; // <- Inyectado para el recálculo

        public UpdateSalesRouteHandler(
            ISalesRouteRepository salesRouteRepository,
            IOrderRepository orderRepository,
            ICityRepository cityRepository)
        {
            _salesRouteRepository = salesRouteRepository;
            _orderRepository = orderRepository;
            _cityRepository = cityRepository;
        }

        public async Task<bool> Handle(UpdateSalesRouteCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos la ruta en la base de datos
            var route = await _salesRouteRepository.GetByIdAsync(request.Id);

            if (route == null)
            {
                throw new Exception("La ruta comercial no existe.");
            }

            // 2. Verificamos si tiene operaciones ligadas
            bool hasOrders = await _orderRepository.HasAnyOrderWithRouteAsync(request.Id);

            if (hasOrders)
            {
                // Defensa en profundidad
                if (request.CityId.HasValue && route.CityId != request.CityId.Value)
                {
                    throw new InvalidOperationException("Esta ruta ya tiene operaciones registradas. No puedes cambiar su ciudad, solo su nombre.");
                }

                // Actualizamos solo la etiqueta visual
                route.UpdateName(request.Name);
            }
            else
            {
                // 3. Lógica para ruta "virgen"
                // Verificamos si realmente nos enviaron una ciudad nueva y distinta a la actual
                if (request.CityId.HasValue && request.CityId.Value != route.CityId)
                {
                    // Al cambiar la ciudad, DEBEMOS recalcular el código interno
                    var newCity = await _cityRepository.GetByIdAsync(request.CityId.Value);
                    if (newCity == null)
                    {
                        throw new Exception("La ciudad seleccionada no existe.");
                    }

                    // Asumiendo que tienes un método en tu repo para contar las rutas por ciudad
                    int currentCount = await _salesRouteRepository.CountByCityAsync(newCity.Id);

                    string newCode = $"{newCity.State.GetPrefix()}-{(currentCount + 1).ToString("D2")}";

                    // Actualizamos con la nueva ciudad y el nuevo código generado
                    route.Update(request.Name, newCity.Id, newCode);
                }
                else
                {
                    // La ruta es virgen, pero la ciudad NO cambió. 
                    // Solo guardamos el nombre nuevo y mantenemos la ciudad y el código intactos.
                    route.Update(request.Name, route.CityId, route.Code);
                }
            }

            // 4. Persistimos los cambios
            await _salesRouteRepository.UpdateAsync(route);
            await _salesRouteRepository.SaveChangesAsync();

            return true;
        }
    }
}