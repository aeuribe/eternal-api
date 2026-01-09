namespace eternal_api.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public City(string name, string state, string country)
        {
            Name = name;
            State = state;
            Country = country;
        }

        public void Update(string name, string state, string country)
        {
            Name = name;
            State = state;
            Country = country;
        }
    }
}
