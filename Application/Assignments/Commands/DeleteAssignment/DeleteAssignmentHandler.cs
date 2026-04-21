using eternal_api.Application.Assignments.Interfaces;
using MediatR;

namespace eternal_api.Application.Assignments.Commands.DeleteAssignment
{
    public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignmentCommand, bool>
    {
        private IAssignmentRepository _assignmentRepository;

        public DeleteAssignmentHandler(IAssignmentRepository assignmentRepository) 
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<bool> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(command.Id);
            if (assignment == null)
            {
                return false;
            } 
            await _assignmentRepository.DeleteAsync(assignment.Id);
            return true;
        }
    }
}
