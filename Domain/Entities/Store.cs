namespace eternal_api.Domain.Entities
{
    public class Store
    {
        // ID único de la tienda
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nombre de la tienda
        public string Name { get; set; }

        // Dirección de la tienda
        public string Address { get; set; }

        // Validación de estatus de la tienda
        public bool IsActive { get; set; }

        // Relación con la entidad City
        public Guid CityId { get; set; }

        public Store(string name, string address, Guid cityId)
        {
            Name = name;
            Address = address;
            CityId = cityId;
            IsActive = true;
        }

        public void Update(string name, string address, Guid cityId)
        {
            Name = name;
            Address = address;
            CityId = cityId;
        }

        public void Desactivate()
        {
            IsActive = false;
        }
    }
}
