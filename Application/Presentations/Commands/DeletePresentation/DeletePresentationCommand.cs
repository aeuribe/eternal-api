using MediatR;

namespace eternal_api.Application.Presentations.Commands.DeletePresentation
{
    public class DeletePresentationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
