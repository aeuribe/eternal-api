using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
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
