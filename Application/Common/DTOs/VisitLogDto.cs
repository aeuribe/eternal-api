namespace eternal_api.Application.VisitLogs.Common
{
    public class VisitLogDto
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid SalespersonId { get; set; }
        public DateOnly VisitDate { get; set; }
    }
}
