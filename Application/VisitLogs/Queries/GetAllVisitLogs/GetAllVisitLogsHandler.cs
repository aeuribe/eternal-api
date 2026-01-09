using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.VisitLogs.Common;
using MediatR;

namespace eternal_api.Application.VisitLogs.Queries.ListVisitLogs
{
    public class GetAllVisitLogsHandler : IRequestHandler<GetAllVisitLogsQuery, IEnumerable<VisitLogDto>>
    {
        private readonly IVisitLogRepository _visitLogsRepository;

        public GetAllVisitLogsHandler(IVisitLogRepository visitLogsRepository)
        {
            _visitLogsRepository = visitLogsRepository;
        }

        public async Task<IEnumerable<VisitLogDto>> Handle(GetAllVisitLogsQuery query, CancellationToken cancellationToken)
        {
            var visits = await _visitLogsRepository.ListAsync();

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
