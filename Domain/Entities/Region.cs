namespace eternal_api.Domain.Entities
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid AreaId { get; set; }
        public Area Area { get; set; }

        public Region() { }
        public Region(Guid areaId, string name)
        {
            Id = Guid.NewGuid();
            AreaId = areaId;
            Name = name;
        }

        public void Update(Guid areaId, string name)
        {
            AreaId = areaId;
            Name = name;
        }
    }
}
