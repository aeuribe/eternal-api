using eternal_api.Application.Identity.DTOs;
using MediatR;

namespace eternal_api.Application.Identity.Command.Login
{
    // El IRequest ahora devuelve nuestro objeto LoginResponse
    public class LoginCommand : IRequest<IdentityUserDto?>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
