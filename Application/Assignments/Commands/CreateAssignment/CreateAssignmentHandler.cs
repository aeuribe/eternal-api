using eternal_api.Application.Assignments.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentHandler : IRequestHandler<CreateAssignmentCommand, Guid>
    {
        private readonly IAssignmentRepository _assignmentRepository;
 
        public CreateAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<Guid> Handle(CreateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var assignment = new Assignment(command.RouteId, command.StoreId);
            return await _assignmentRepository.AddAsync(assignment);
        }
    }
}
