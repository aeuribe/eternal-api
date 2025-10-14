using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Users.Commands.DeactivateUser
{
    public class DesactivateUserHandler
    {
        private readonly IUserRepository _repo;

        public DesactivateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DesactivateUserCommand command)
        {
            var user = await _repo.GetByIdAsync(command.Id);
            if (user is null) return false;

            user.IsActive = false;
            await _repo.UpdateAsync(user);
            return true;
        }
    }
}
