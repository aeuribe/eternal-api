using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Users.Commands.DeactivateUser
{
    public class DesactivateUserHandler : IRequestHandler<DesactivateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DesactivateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DesactivateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user is null) return false;

            user.Desactivate();

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
