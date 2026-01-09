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
        public Guid ProductId { get; set; }

        public HistPrice(Guid productId, decimal value, DateTime startDate, DateTime? endDate = null)
        {
            ProductId = productId;
            Price = value;
            StartDate = startDate;
            EndDate = endDate;
        }
        public HistPrice() { }
        
    }
}
