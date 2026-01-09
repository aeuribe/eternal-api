using MediatR;

namespace eternal_api.Application.VisitLogs.Commands.DeleteVisitLog
{
    public class DeleteVisitLogCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
