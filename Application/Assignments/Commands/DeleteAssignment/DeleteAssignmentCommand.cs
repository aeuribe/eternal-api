using MediatR;

namespace eternal_api.Application.Assignments.Commands.DeleteAssignment
{
    public class DeleteAssignmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
