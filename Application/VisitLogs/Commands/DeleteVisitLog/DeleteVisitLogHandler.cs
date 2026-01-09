using eternal_api.Application.Common.Interfaces;
using eternal_api.Infrastructure.Persistence;
using MediatR;


namespace eternal_api.Application.VisitLogs.Commands.DeleteVisitLog
{
    public class DeleteVisitLogHandler : IRequestHandler<DeleteVisitLogCommand, bool>
    {
        private readonly IVisitLogRepository _visitLogRepository;

        public DeleteVisitLogHandler(IVisitLogRepository visitLogRepository)
        {
            _visitLogRepository = visitLogRepository;
        }

        public async Task<bool> Handle(DeleteVisitLogCommand command, CancellationToken cancellationToken)
        {
            return await _visitLogRepository.DeleteAsync(command.Id);
        }
    }
}
