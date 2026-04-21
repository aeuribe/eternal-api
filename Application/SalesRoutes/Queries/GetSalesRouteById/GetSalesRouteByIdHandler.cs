using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.SalesRoutes.DTOs;
using eternal_api.Application.SalesRoutes.Interfaces;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Queries.GetSalesRouteById
{
    public class GetSalesRouteByIdHandler : IRequestHandler<GetSalesRouteByIdQuery, SalesRouteDto?>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;
        private readonly IOrderRepository _orderRepository;

        public GetSalesRouteByIdHandler(
            ISalesRouteRepository salesRouteRepository,
            IOrderRepository orderRepository)
        {
            _salesRouteRepository = salesRouteRepository;
            _orderRepository = orderRepository;
        }

        public async Task<SalesRouteDto?> Handle(GetSalesRouteByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Buscamos la entidad cruda
            var route = await _salesRouteRepository.GetByIdAsync(request.Id);
            if (route == null) return null;

            // 2. Calculamos la bandera al vuelo (Súper rápido gracias a .AnyAsync() en el repo de órdenes)
            bool hasOrders = await _orderRepository.HasAnyOrderWithRouteAsync(request.Id);

            // 3. Mapeamos la Entidad al DTO para Next.js
            return new SalesRouteDto
            {
                Id = route.Id,
                Code = route.Code,
                Name = route.Name,
                CityId = route.CityId,
                IsActive = route.IsActive,
                HasOrders = hasOrders // Inyectamos el cálculo
            };
        }
    }
}