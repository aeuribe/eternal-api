using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.SalesRoutes.DTOs;
using eternal_api.Application.SalesRoutes.Interfaces;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Queries.GetAllSalesRoutes
{
    public class GetAllSalesRoutesHandler : IRequestHandler<GetAllSalesRoutesQuery, IEnumerable<SalesRouteDto>>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;
        private readonly IOrderRepository _orderRepository;

        public GetAllSalesRoutesHandler(
            ISalesRouteRepository salesRouteRepository,
            IOrderRepository orderRepository)
        {
            _salesRouteRepository = salesRouteRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<SalesRouteDto>> Handle(GetAllSalesRoutesQuery request, CancellationToken cancellationToken)
        {
            // 1. Obtenemos todas las rutas
            var routes = await _salesRouteRepository.GetAllAsync();

            var routesDto = new List<SalesRouteDto>();

            // 2. Mapeamos y verificamos órdenes para cada una
            // Nota de rendimiento: Como el catálogo de rutas comerciales (FL-01, FL-02) suele ser pequeño (ej. 10 a 50 rutas), 
            // este bucle es perfectamente aceptable y rápido.
            foreach (var route in routes)
            {
                bool hasOrders = await _orderRepository.HasAnyOrderWithRouteAsync(route.Id);

                routesDto.Add(new SalesRouteDto
                {
                    Id = route.Id,
                    Code = route.Code,
                    Name = route.Name,
                    CityId = route.CityId,
                    IsActive = route.IsActive,
                    HasOrders = hasOrders
                });
            }

            return routesDto;
        }
    }
}