namespace eternal_api.Domain.Entities
{
    public class Store
    {
        // ID único de la tienda
        public int Id { get; set; }

        // Nombre de la tienda
        public string Name { get; set; }

        // Dirección de la tienda
        public string Address { get; set; }

        // Validación de estatus de la tienda
        public bool IsActive { get; set; }

        // Relación con la entidad City
        public int CityId { get; set; }

    }
}
