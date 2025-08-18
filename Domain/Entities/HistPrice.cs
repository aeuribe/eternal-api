namespace eternal_api.Domain.Entities
{
    public class HistPrice
    {
        // ID único del historial de precios
        public int Id { get; set; }

        // Fecha de asignaciön de precios
        public DateTime StartDate { get; set; }

        // Fecha de finalización de precios
        public DateTime? EndDate { get; set; }

        // Precio del producto
        public decimal Price { get; set; } = 0;

        // Referencia foránea al producto
        public int ProductId { get; set; }

    }
}
