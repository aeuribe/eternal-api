using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Users.Commands.UpdateUser
{
    public class UpdateUserHandler
    {
        private readonly IUserRepository _repo;

        public UpdateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateUserCommand command)
        {
            var user = await _repo.GetByIdAsync(command.Id);
            if (user is null) return false;

            user.Name = command.Name;
            user.LastName = command.LastName;
            user.Rol = command.Rol;
            user.Phone = command.Phone;
            user.CityId = command.CityId;

            await _repo.UpdateAsync(user);
            return true;
        }
    }
}
