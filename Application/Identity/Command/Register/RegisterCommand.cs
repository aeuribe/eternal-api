using MediatR;

namespace eternal_api.Application.Identity.Command.Register
{
    public class RegisterCommand : IRequest<Guid>
    {
        // --- Datos para Identity (Esquema security) ---
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // --- Datos para el Usuario de Negocio (Esquema dbo) ---
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Rol { get; set; }
        public string Phone { get; set; } = string.Empty;

    }
}
