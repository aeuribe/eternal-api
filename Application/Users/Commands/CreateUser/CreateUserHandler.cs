using eternal_api.Domain.Entities;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User(command.Name, command.LastName, command.Rol, command.Phone, command.CityId);
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}

