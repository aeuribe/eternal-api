namespace eternal_api.Domain.Entities
{
    public class User
    {
        // ID único del usuario
        public Guid Id { get; set; }

        // Nombre del usuario
        public string Name { get; set; }

        // Apellido del usuario
        public string LastName { get; set; }

        // Rol del usuario (Vendedor o Administrador)
        public string Rol { get; set; }

        // Teléfono del usuario
        public string Phone { get; set; }

        // Relación con la entidad City
        public Guid CityId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
