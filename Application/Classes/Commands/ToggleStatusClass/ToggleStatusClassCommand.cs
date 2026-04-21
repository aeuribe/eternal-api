using MediatR;

namespace eternal_api.Application.Classes.Commands.ToggleStatusClass
{
    public class ToggleStatusClassCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
