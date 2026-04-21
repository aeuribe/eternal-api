using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.SalesRoutes.Command.CreateSalesRoute
{
    public class CreateSalesRouteHandler : IRequestHandler<CreateSalesRouteCommand, Guid>
    {
        private readonly ISalesRouteRepository _salesRouteRepository;
        private readonly ICityRepository _cityRepository;

        public CreateSalesRouteHandler(
            ISalesRouteRepository salesRouteRepository,
            ICityRepository cityRepository)
        {
            _salesRouteRepository = salesRouteRepository;
            _cityRepository = cityRepository;
        }

        public async Task<Guid> Handle(CreateSalesRouteCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar la ciudad
            var city = await _cityRepository.GetByIdAsync(request.CityId);
            if (city == null)
            {
                throw new Exception($"La ciudad especificada no existe en el sistema.");
            }

            var statePrefix = city.State.GetPrefix(); // Ej: "FL"

            // 2. Obtener el último código generado para ese Estado
            var lastCode = await _salesRouteRepository.GetLastCodeByStatePrefixAsync(statePrefix);

            int nextNumber = 1;

            // 3. Si ya existe un código (ej. "FL-05"), extraemos el "05" y le sumamos 1
            if (!string.IsNullOrEmpty(lastCode))
            {
                // Separamos por el guion y tomamos la segunda parte
                var parts = lastCode.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            // 4. Generar el nuevo código comercial (Ej: FL-06)
            string newCode = $"{statePrefix}-{nextNumber.ToString("D2")}";

            // 5. Instanciar y persistir
            var newSalesRoute = new SalesRoute(request.Name, newCode, request.CityId);

            await _salesRouteRepository.AddAsync(newSalesRoute);
            await _salesRouteRepository.SaveChangesAsync();

            return newSalesRoute.Id;
        }
    }
}