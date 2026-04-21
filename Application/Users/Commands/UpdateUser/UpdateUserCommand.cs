using MediatR;

namespace eternal_api.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}

