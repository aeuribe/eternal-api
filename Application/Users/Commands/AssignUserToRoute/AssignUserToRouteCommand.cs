using MediatR;

namespace eternal_api.Application.Users.Commands.AssignUserToRoute
{
    public class AssignUserToRouteCommand : IRequest<bool>
    {
        // ¿A quién vamos a modificar?
        public Guid UserId { get; set; }

        // ¿Qué le vamos a hacer? 
        // Si trae un Guid -> Le asignamos esa ruta.
        // Si trae null   -> Lo dejamos "sin ruta" (Desasignar).
        public Guid? RouteId { get; set; }
    }
}