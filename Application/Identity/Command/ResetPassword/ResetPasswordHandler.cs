using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using eternal_api.Application.Identity.Interfaces;

namespace eternal_api.Application.Identity.Command.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, (bool Succeeded, string[] Errors)>
    {
        private readonly IIdentityRepository _identityRepo;

        public ResetPasswordHandler(IIdentityRepository identityRepo)
        {
            _identityRepo = identityRepo;
        }

        public async Task<(bool Succeeded, string[] Errors)> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // 1. Decodificar el token que viene de la URL (importante para que Identity lo reconozca)
            string decodedToken;
            try
            {
                var decodedBytes = WebEncoders.Base64UrlDecode(request.Token);
                decodedToken = Encoding.UTF8.GetString(decodedBytes);
            }
            catch
            {
                return (false, new[] { "El formato del token de seguridad es inválido." });
            }

            // 2. Llamar al repositorio para validar y cambiar la contraseña
            return await _identityRepo.ResetPasswordAsync(request.Email, decodedToken, request.NewPassword);
        }
    }
}