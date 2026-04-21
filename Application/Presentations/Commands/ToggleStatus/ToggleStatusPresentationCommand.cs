using MediatR;

namespace eternal_api.Application.Presentations.Commands.ToggleStatus
{
    public class ToggleStatusPresentationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public ToggleStatusPresentationCommand(Guid id)
        {
            Id = id;
        }
    }
}