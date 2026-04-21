using MediatR;

namespace eternal_api.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentCommand : IRequest<Guid>
    {
        public Guid RouteId { get; set; }
        public Guid StoreId { get; set; }
    }
}
