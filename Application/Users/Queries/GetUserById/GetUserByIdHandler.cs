using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Users.Queries.GetUserById
{
    public class GetUserByIdHandler
    {
        private readonly IUserRepository _repo;

        public GetUserByIdHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery query)
        {
            var user = await _repo.GetByIdAsync(query.Id);
            return user is null ? null : new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Rol = user.Rol,
                Phone = user.Phone,
                CityId = user.CityId,
                IsActive = user.IsActive
            };
        }
    }
}
