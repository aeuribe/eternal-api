using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.VisitLogs.Commands.CreateVisitLog
{
    public class CreateVisitLogHandler
    {
        private readonly IVisitLogRepository _repository;

        public CreateVisitLogHandler(IVisitLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateVisitLogCommand command)
        {
            var entity = new VisitLog
            {
                Id = Guid.NewGuid(),
                StoreId = command.StoreId,
                SalespersonId = command.SalespersonId,
                VisitDate = command.VisitDate
            };

            return await _repository.AddAsync(entity);
        }
    }
}
