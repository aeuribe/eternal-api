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

        public string Name { get; set; }
        public string Description { get; set; }

        public Planogram(string name, string description)
        {
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description) 
        {
            Name = name;
            Description = description;
        }

        public void Desactivate()
        {
            isActive = false;
        }
    }
}
