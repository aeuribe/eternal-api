using eternal_api.Application.Identity.Interfaces;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Application.Users.Queries.DTOs;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityRepository _identityRepository;

        public GetUserByIdHandler(IUserRepository userRepository, IIdentityRepository identityRepository)
        {
            _userRepository = userRepository;
            _identityRepository = identityRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(query.Id);
            if (user == null)
            {
                return null;
            }

            var (succeeded, email, role, errors) = await _identityRepository.GetUserAsync(user.IdentityUserId);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Rol = user.Rol,    
                Email = email, 
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

            };
        }
    }
}
