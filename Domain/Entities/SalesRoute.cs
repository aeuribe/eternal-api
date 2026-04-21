namespace eternal_api.Domain.Entities
{
    public class SalesRoute
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public Guid CityId { get; set; }
        public City City {get; set;}

        public SalesRoute() { }

        public SalesRoute(string name, string code, Guid cityId) 
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            CityId = cityId;
            IsActive = true;
        }

        public void Update(string name, Guid cityId, string code)
        {
            Name = name;
            CityId = cityId;
            Code = code;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void ToogleStatus()
        {
            IsActive = !IsActive;
        }
    }
}
