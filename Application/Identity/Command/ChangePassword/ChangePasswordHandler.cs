using eternal_api.Application.Identity.Interfaces;
using MediatR;

namespace eternal_api.Application.Identity.Command.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IIdentityRepository _identityRepository;

        public ChangePasswordHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            // El repositorio de identidad valida la contraseña actual y aplica la nueva
            var (succeeded, errors) = await _identityRepository.ChangePasswordAsync(
                command.Email,
                command.CurrentPassword,
                command.NewPassword
            );

            if (!succeeded)
            {
                // Podrías registrar los logs de 'errors' si fuera necesario para auditoría
                return false;
            }

            return true;
        }
    }
}
