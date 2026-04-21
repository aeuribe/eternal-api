namespace eternal_api.Domain.Entities
{
    public class District
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        
        public District() { }

        public District(Guid regionId, string name)
        {
            Id = Guid.NewGuid();
            RegionId = regionId;
            Name = name;
        }

        public void Update(Guid regionId, string name)
        {
            RegionId = regionId;
            Name = name;
        }
    }
}
