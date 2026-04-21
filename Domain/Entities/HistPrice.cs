namespace eternal_api.Domain.Entities
{
    public class HistPrice
    {
        // ID único del historial de precios
        public Guid Id { get; set; } = Guid.NewGuid();

        // Fecha de asignaciön de precios
        public DateTime StartDate { get; set; }

        // Fecha de finalización de precios
        public DateTime? EndDate { get; set; }

        // Precio del producto
        public decimal Price { get; set; } = 0;

        // Referencia foránea al producto
        public Guid PresentationId { get; set; }

        public HistPrice(Guid presentationId, decimal value, DateTime startDate, DateTime? endDate )
        {
            PresentationId = presentationId;
            Price = value;
            StartDate = startDate;
            EndDate = endDate;
        }
        public HistPrice() { }
        
    }
}
