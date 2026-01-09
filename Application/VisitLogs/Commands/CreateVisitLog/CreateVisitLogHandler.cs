using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.VisitLogs.Commands.CreateVisitLog
{
    public class CreateVisitLogHandler : IRequestHandler<CreateVisitLogCommand, Guid>
    {
        private readonly IVisitLogRepository _visitLogrepository;

        public CreateVisitLogHandler(IVisitLogRepository visitLogrepository)
        {
            _visitLogrepository = visitLogrepository;
        }

        public async Task<Guid> Handle(CreateVisitLogCommand command, CancellationToken cancellationToken)
        {
            var visitLog = new VisitLog(command.VisitDate, command.SalespersonId, command.StoreId);
            return await _visitLogrepository.AddAsync(visitLog);
        }
    }
}
