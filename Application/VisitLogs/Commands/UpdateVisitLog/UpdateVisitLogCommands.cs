using MediatR;

namespace eternal_api.Application.VisitLogs.Commands.UpdateVisitLog
{
    public class UpdateVisitLogCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid SalespersonId { get; set; }
        public DateOnly VisitDate { get; set; }
    }
}
