using MediatR;

namespace eternal_api.Application.Identity.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<(bool Succeeded, string[] Errors)>
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}