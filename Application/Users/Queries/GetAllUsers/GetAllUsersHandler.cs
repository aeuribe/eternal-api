using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using MediatR;

namespace eternal_api.Application.Users.Queries.ListUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery ,IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync();
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
