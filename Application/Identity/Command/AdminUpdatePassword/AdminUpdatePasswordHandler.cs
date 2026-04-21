using eternal_api.Application.Identity.Interfaces;
using MediatR;

namespace eternal_api.Application.Identity.Command.AdminUpdatePassword
{
    public class AdminUpdatePasswordHandler : IRequestHandler<AdminUpdatePasswordCommand, (bool Succeeded, string[] Errors)>
    {
        private readonly IIdentityRepository _identityRepository;

        public AdminUpdatePasswordHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<(bool Succeeded, string[] Errors)> Handle(AdminUpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            // Devolvemos toda la tupla (el Succeeded y los Errors)
            return await _identityRepository.AdminUpdateUserPasswordAsync(request.UserId, request.NewPassword);
        }
    }
}