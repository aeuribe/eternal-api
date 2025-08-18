namespace eternal_api.Domain.Entities
{
    public class Planogram
    {
        // ID único del planograma
        public int Id { get; set; }

        // Fecha de Creacion del planograma
        public DateTime CreatedAt { get; set; }

        // Status del planograma
        public bool isActive { get; set; } = true;
    }
}
