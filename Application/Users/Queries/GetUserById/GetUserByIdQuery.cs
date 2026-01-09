using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
