using eternal_api.Application.Identity.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace eternal_api.Infraestructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // Extraemos la configuración que ya tienes en tu JSON
            var apiKey = _configuration["SendGrid:ApiKey"];
            var fromEmail = _configuration["SendGrid:FromEmail"];
            var fromName = _configuration["SendGrid:FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var target = new EmailAddress(to);

            // Creamos el mensaje. Usamos 'body' tanto para texto plano como para HTML
            var msg = MailHelper.CreateSingleEmail(from, target, subject, body, body);

            var response = await client.SendEmailAsync(msg);

            // Es buena práctica de ingeniería validar que el envío fue exitoso
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new Exception($"Error enviando email vía SendGrid: {response.StatusCode} - {errorBody}");
            }
        }
    }
}