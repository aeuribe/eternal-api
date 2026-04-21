using eternal_api.Application.Users.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Users.Queries.ListUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>> { }
}
