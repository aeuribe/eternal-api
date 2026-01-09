using MediatR;

namespace eternal_api.Application.Users.Commands.DeactivateUser
{
    public class DesactivateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
