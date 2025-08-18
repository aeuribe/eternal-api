namespace eternal_api.Domain.Entities
{
    public class VisitLog
    {
        // ID único del registro de visita
        public int Id { get; set; }

        // Fecha y hora de la visita
        public DateTime VisitDate { get; set; }

        // Referencias foraneas de la entidad
        public int SalespersonId { get; set; }
        public int StoreId { get; set; }    
    }
}
