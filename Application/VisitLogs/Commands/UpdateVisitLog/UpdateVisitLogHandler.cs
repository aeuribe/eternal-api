using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.VisitLogs.Commands.UpdateVisitLog
{
    public class UpdateVisitLogHandler
    {
        private readonly IVisitLogRepository _repo;

        public UpdateVisitLogHandler(IVisitLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateVisitLogCommand command)
        {
            var visit = new VisitLog
            {
                Id = command.Id,
                StoreId = command.StoreId,
                SalespersonId = command.SalespersonId,
                VisitDate = command.VisitDate
            };

            return await _repo.UpdateAsync(visit);
        }
    }
}
