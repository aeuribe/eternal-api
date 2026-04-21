using eternal_api.Domain.Enums;

namespace eternal_api.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public StateUsEnum State { get; set; }

        public City() { }

        // Firmas limpias solo con lo necesario
        public City(string name, StateUsEnum state)
        {
            Name = name ?? string.Empty;
            State = state;
        }

        public void Update(string name, StateUsEnum state)
        {
            Name = name ?? string.Empty;
            State = state;
        }
    }
}