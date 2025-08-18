namespace eternal_api.Domain.Entities
{
    public class Bill
    {
        // ID único de bill
        public int Id { get; set; }

        // Fecha de creación
        public DateTime CreatedAt { get; set; }
        
        // Monto total
        public decimal Total { get; set; }

        // Dirección de la imagen
        public string ImageUrl { get; set; }

        // Clave foránea de la orden asociada
        public int OrderId { get; set; }

        // Clave foranea de POD asociado
        public int PodId { get; set; }

    }
}
