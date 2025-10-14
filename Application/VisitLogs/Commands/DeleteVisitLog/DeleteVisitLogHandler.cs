using eternal_api.Application.Common.Interfaces;
using eternal_api.Infrastructure.Persistence;


namespace eternal_api.Application.VisitLogs.Commands.DeleteVisitLog
{
    public class DeleteVisitLogHandler
    {
        private readonly IVisitLogRepository _repo;

        public DeleteVisitLogHandler(IVisitLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteVisitLogCommand command)
        {
            return await _repo.DeleteAsync(command.Id);
        }
    }
}
