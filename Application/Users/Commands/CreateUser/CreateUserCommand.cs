using MediatR;

namespace eternal_api.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Rol { get; set; } = "Vendedor";
        public string Phone { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
