using eternal_api.Domain.Entities;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Users.Commands.CreateUser
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _repo;

        public CreateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateUserCommand command)
        {
            var user = new User
            {
                Name = command.Name,
                LastName = command.LastName,
                Rol = command.Rol,
                Phone = command.Phone,
                CityId = command.CityId,
                IsActive = command.IsActive
            };

            await _repo.AddAsync(user);
            return user.Id;
        }
    }
}

