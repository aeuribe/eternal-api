using MediatR;

namespace eternal_api.Application.Identity.Command.GenerateTokenPasswordReset
{
    public class GenerateTokenPasswordResetCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
        // Agregamos esto para saber a qué PWA redirigir
        public string AppType { get; set; } = string.Empty; // "Admin" o "Vendedor"
    }
}
