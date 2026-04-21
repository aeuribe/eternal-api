using eternal_api.Application.Identity.DTOs;
using eternal_api.Application.Identity.Interfaces;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.Identity.Command.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, IdentityUserDto?>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUserIdentityProvider _userProvider;
        public LoginHandler(IIdentityRepository identityRepository, IUserIdentityProvider userProvider)
        {
            _identityRepository = identityRepository;
            _userProvider = userProvider;
        }

        public async Task<IdentityUserDto?> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // Llamamos al servicio de identidad (implementado en Infraestructura)
            var (succeeded, token, userId, errors) = await _identityRepository.LoginAsync(command.Email, command.Password);
            var userBusinessData = await _userProvider.GetBasicProfileAsync(userId);

            if (!succeeded || token == null)
            {
                // Aquí podrías lanzar una excepción personalizada o manejar el error
                return null;
            }

            return new IdentityUserDto
            {
                Token = token,
                Email = command.Email,
                Name = userBusinessData.Name,
                LastName = userBusinessData.LastName
            };
        }
    }
}
