using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Users.Queries.ListUsers
{
    public class ListUsersHandler
    {
        private readonly IUserRepository _repo;

        public ListUsersHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserDto>> Handle(ListUsersQuery query)
        {
            var users = await _repo.ListAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Rol = u.Rol,
                Phone = u.Phone,
                CityId = u.CityId,
                IsActive = u.IsActive
            }).ToList();
        }
    }
}
