using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Application.Users.Interfaces; // Asegúrate de tener esta interfaz
using MediatR;

namespace eternal_api.Application.Users.Commands.AssignUserToRoute
{
    public class AssignUserToRouteHandler : IRequestHandler<AssignUserToRouteCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISalesRouteRepository _salesRouteRepository;

        public AssignUserToRouteHandler(
            IUserRepository userRepository,
            ISalesRouteRepository salesRouteRepository)
        {
            _userRepository = userRepository;
            _salesRouteRepository = salesRouteRepository;
        }

        public async Task<bool> Handle(AssignUserToRouteCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos al vendedor en la base de datos
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("El usuario especificado no existe.");
            }

            // 2. Evaluamos la intención del Frontend (Next.js)
            if (request.RouteId.HasValue)
            {
                // Intención: ASIGNAR o CAMBIAR de ruta

                // Validación de integridad: ¿La ruta que intentan asignarle realmente existe?
                var route = await _salesRouteRepository.GetByIdAsync(request.RouteId.Value);
                if (route == null)
                {
                    throw new Exception("La ruta comercial que intentas asignar no existe o fue eliminada.");
                }

                // Usamos el método expresivo de tu Dominio
                user.AssignRoute(request.RouteId.Value);
            }
            else
            {
                // Intención: QUITAR ruta (Desasignar)
                // Usamos el otro método expresivo
                user.RemoveRoute();
            }

            // 3. Persistimos los cambios en PostgreSQL
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}