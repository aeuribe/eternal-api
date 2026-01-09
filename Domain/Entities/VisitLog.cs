namespace eternal_api.Domain.Entities
{
    public class VisitLog
    {
        // ID único del registro de visita
        public Guid Id { get; set; } = Guid.NewGuid();

        // Fecha y hora de la visita
        public DateOnly VisitDate { get; set; }

        // Referencias foraneas de la entidad
        public Guid SalespersonId { get; set; }
        public Guid StoreId { get; set; }

        public Store? Store { get; set; }
        public User? Salesperson { get; set; }

        public VisitLog(DateOnly visitDate, Guid salespersonId, Guid storeId)
        {
            VisitDate = visitDate;
            SalespersonId = salespersonId;
            StoreId = storeId;
        }

        public void Update(DateOnly visitDate, Guid salespersonId, Guid storeId)
        {
            VisitDate = visitDate;
            SalespersonId = salespersonId;
            StoreId = storeId;
        }
    }
}
