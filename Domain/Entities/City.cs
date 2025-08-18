namespace eternal_api.Domain.Entities
{
    public class City
    {
        // ID único de ciudad
        public int Id { get; set; }

        // Nombre de la ciudad
        public string Name { get; set; }

        // Estado o provincia donde se encuentra la ciudad
        public string State { get; set; }
    }
}
