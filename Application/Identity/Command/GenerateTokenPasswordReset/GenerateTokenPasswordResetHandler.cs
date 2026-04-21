using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.Extensions.Configuration;
using eternal_api.Application.Identity.Services;
using eternal_api.Application.Identity.Interfaces;

namespace eternal_api.Application.Identity.Command.GenerateTokenPasswordReset
{
    public class GenerateTokenPasswordResetHandler : IRequestHandler<GenerateTokenPasswordResetCommand, bool>
    {
        private readonly IIdentityRepository _identityRepo;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public GenerateTokenPasswordResetHandler(
            IIdentityRepository identityRepo,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _identityRepo = identityRepo;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> Handle(GenerateTokenPasswordResetCommand request, CancellationToken cancellationToken)
        {
            // 1. Generar el Token de Identity
            var token = await _identityRepo.GeneratePasswordResetTokenAsync(request.Email);
            if (string.IsNullOrEmpty(token)) return false;

            // 2. Codificar el token para que sea seguro en una URL
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // 3. Determinar la URL base según la aplicación (Admin o Vendedor)
            string baseUrl = request.AppType == "Admin"
                ? _configuration["SendGrid:AdminBaseUrl"]
                : _configuration["SendGrid:VendedoresBaseUrl"];

            // 4. Construir el link final
            var resetLink = $"{baseUrl}/reset-password?token={encodedToken}&email={request.Email}";

            // 5. Enviar el correo con SendGrid
            await _emailService.SendEmailAsync(
                request.Email,
                "Restablecer Contraseña - Order It System",
                $"<p>Has solicitado restablecer tu contraseña en <strong>Order It System</strong>.</p>" +
                $"<p>Haz clic en el siguiente enlace para continuar:</p>" +
                $"<a href='{resetLink}'>Restablecer Contraseña</a>");

            return true;
        }
    }
}