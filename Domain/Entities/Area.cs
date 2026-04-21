namespace eternal_api.Domain.Entities
{
    public class Area
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Area() { }
        public Area(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

    }
}
