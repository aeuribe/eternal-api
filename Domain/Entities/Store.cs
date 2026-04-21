namespace eternal_api.Domain.Entities
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string StoreNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string? Name { get; set; }
        public string Street { get; set; }
        public bool IsActive { get; set; }
        public bool HasPlanogram { get; set; }

        // Relación con la entidad City
        public Guid CityId { get; set; }
        public City City { get; set; }

        public Guid DistrictId { get; set; }
        public District District { get; set; }

        public Store() { }

        // Constructor para la creación inicial (IsActive = true por defecto)
        public Store(string storeNumber, string zoneNumber, string zipCode, string? name, string street, bool hasPlanogram, Guid cityId, Guid districtId)
        {
            StoreNumber = storeNumber;
            ZoneNumber = zoneNumber;
            ZipCode = zipCode;
            Name = name;
            Street = street;
            IsActive = true;
            HasPlanogram = hasPlanogram;
            CityId = cityId;
            DistrictId = districtId;
        }

        // Método Update para la modificación de datos
        public void Update(string storeNumber, string zoneNumber, string zipCode, string? name, string street, bool hasPlanogram, Guid cityId, Guid districtId)
        {
            StoreNumber = storeNumber;
            ZoneNumber = zoneNumber;
            ZipCode = zipCode;
            Name = name;
            Street = street;
            HasPlanogram = hasPlanogram;
            CityId = cityId;
            DistrictId = districtId;
        }

        public void Desactivate()
        {
            IsActive = !IsActive;
        }
    }
}
