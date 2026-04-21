using MediatR;

namespace eternal_api.Application.Identity.Command.AdminUpdatePassword
{
    // Cambiamos el tipo de retorno para incluir los Errores
    public class AdminUpdatePasswordCommand : IRequest<(bool Succeeded, string[] Errors)>
    {
        public string UserId { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}