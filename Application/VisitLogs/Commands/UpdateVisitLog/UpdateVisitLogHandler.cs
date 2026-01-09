using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.VisitLogs.Commands.UpdateVisitLog
{
    public class UpdateVisitLogHandler : IRequestHandler<UpdateVisitLogCommand, bool>
    {
        private readonly IVisitLogRepository _visitLogRepository;

        public UpdateVisitLogHandler(IVisitLogRepository visitLogRepository)
        {
            _visitLogRepository = visitLogRepository;
        }

        public async Task<bool> Handle(UpdateVisitLogCommand command, CancellationToken cancellationToken)
        {
            var visitLog = await _visitLogRepository.GetVisitLogByIdAsync(command.Id);
            return await _visitLogRepository.UpdateAsync(visitLog);
        }
    }
}
