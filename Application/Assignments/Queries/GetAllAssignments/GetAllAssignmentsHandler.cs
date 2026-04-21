using eternal_api.Application.Assignments.Queries.DTOs;
using eternal_api.Application.Assignments.Interfaces;
using MediatR;

namespace eternal_api.Application.Assignments.Queries.GetAllVisitLogs
{
    public class GetAllAssignmentsHandler : IRequestHandler<GetAllAssignmentsQuery, IEnumerable<AssignmentDto>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
    
        public GetAllAssignmentsHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<IEnumerable<AssignmentDto>> Handle(GetAllAssignmentsQuery query, CancellationToken cancellationToken)
        {
            var assignments = await _assignmentRepository.ListAsync();

            return assignments.Select(a => new AssignmentDto
            {
                Id = a.Id,
                SalesRouteId = a.SalesRouteId,
                StoreId = a.StoreId,
                AssignedAt = a.AssignedAt
            }).ToList();
        }
    }
}
