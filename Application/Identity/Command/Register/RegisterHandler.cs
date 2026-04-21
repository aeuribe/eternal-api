using eternal_api.Application.Identity.Interfaces;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Identity.Command.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUserRegistrationService _userRegistrationService;

        // Limpiamos los providers viejos
        public RegisterHandler(
            IIdentityRepository identityRepository,
            IUserRegistrationService userService)
        {
            _identityRepository = identityRepository;
            _userRegistrationService = userService;
        }

        public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _identityRepository.EmailExistsAsync(command.Email))
            {
                throw new Exception("El correo electrónico ya se encuentra registrado en el sistema.");
            }

            if (string.IsNullOrWhiteSpace(command.Email) ||
                string.IsNullOrWhiteSpace(command.Password) ||
                string.IsNullOrWhiteSpace(command.Name) ||
                string.IsNullOrWhiteSpace(command.LastName) ||
                string.IsNullOrWhiteSpace(command.Phone) ||
                string.IsNullOrWhiteSpace(command.Rol))
            {
                throw new Exception("Campos vacíos o inválidos.");
            }

            // 1. Crear el usuario en Identity (security)
            var (succeeded, identityUserId, errors) = await _identityRepository.CreateUserAsync(
                command.Email, command.Password, command.Rol
            );

            if (!succeeded)
            {
                throw new Exception($"Error al crear credenciales de acceso: {string.Join(", ", errors)}");
            }

            // 2. Crear la entidad User (dbo). ¡Ya no pasamos códigos raros, solo la data pura!
            var user = new User(
                command.Name,
                command.LastName,
                command.Rol,
                command.Phone,
                identityUserId
            );

            // 3. Persistir
            await _userRegistrationService.AddAsync(user);

            return user.Id;
        }
    }
}