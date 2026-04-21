using eternal_api.Application.Assignments.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Assignments.Queries.GetAllVisitLogs
{
    public class GetAllAssignmentsQuery : IRequest<IEnumerable<AssignmentDto>> { }
}
