using eternal_api.Application.Users.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user is null) return false;

            user.Update(command.Name, command.LastName, command.Rol, command.Phone);

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
