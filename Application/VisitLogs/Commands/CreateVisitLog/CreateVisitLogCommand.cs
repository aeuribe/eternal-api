namespace eternal_api.Application.VisitLogs.Commands.CreateVisitLog
{
    public class CreateVisitLogCommand
    {
        public Guid StoreId { get; set; }
        public Guid SalespersonId { get; set; }
        public DateOnly VisitDate { get; set; }
    }
}
