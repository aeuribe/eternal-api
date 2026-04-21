using eternal_api.Application.Identity.Interfaces;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Application.Users.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Users.Queries.ListUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery ,IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityRepository _identityRepository;

        public GetAllUsersHandler(IUserRepository userRepository, IIdentityRepository identityRepository)
        {
            _userRepository = userRepository;
            _identityRepository = identityRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var (succeeded, email, role, errors) = await _identityRepository.GetUserAsync(user.IdentityUserId);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Rol = user.Rol,
                    Email = email, // Aquí asignamos el email recuperado
                    Phone = user.Phone,
                    IsActive = user.IsActive,
                    IdentityUserId = user.IdentityUserId,

                    // 1. Asignamos el ID directamente
                    SalesRouteId = user.SalesRouteId,

                    // 2. Mapeamos el DTO anidado verificando que la entidad tenga la ruta cargada
                    SalesRoute = user.SalesRoute != null ? new BaseSalesRouteDto
                    {
                        Id = user.SalesRoute.Id,
                        Name = user.SalesRoute.Name,
                        Code = user.SalesRoute.Code
                    } : null

                });
            }

            return userDtos;
        }
    }
}
