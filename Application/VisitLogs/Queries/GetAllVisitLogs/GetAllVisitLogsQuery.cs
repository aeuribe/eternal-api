using eternal_api.Application.VisitLogs.Common;
using MediatR;

namespace eternal_api.Application.VisitLogs.Queries.ListVisitLogs
{
    public class GetAllVisitLogsQuery : IRequest<IEnumerable<VisitLogDto>> { }
}
