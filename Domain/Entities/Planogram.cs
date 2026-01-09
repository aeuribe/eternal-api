namespace eternal_api.Domain.Entities
{
    public class Planogram
    {
        // ID único del planograma
        public Guid Id { get; set; } = Guid.NewGuid();

        // Fecha de Creacion del planograma
        public DateTime CreatedAt { get; set; }

        // Status del planograma
        public bool isActive { get; set; } = true;

        public Planogram()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void SwitchStatus()
        {
            if (isActive == true)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }
        }
    }
}
