using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.VisitLogs.Common;

namespace eternal_api.Application.VisitLogs.Queries.ListVisitLogs
{
    public class ListVisitLogsHandler
    {
        private readonly IVisitLogRepository _repo;

        public ListVisitLogsHandler(IVisitLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<VisitLogDto>> Handle(ListVisitLogsQuery query)
        {
            var visits = await _repo.ListAsync();

            return visits.Select(v => new VisitLogDto
            {
                Id = v.Id,
                StoreId = v.StoreId,
                SalespersonId = v.SalespersonId,
                VisitDate = v.VisitDate
            }).ToList();
        }
    }
}
