namespace eternal_api.Application.Users.Queries.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rol { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string IdentityUserId { get; set; }

        // Agregamos el ID de la ruta (anulable por si es un admin o alguien de planta)
        public Guid? SalesRouteId { get; set; }

        // Opcional pero MUY recomendado para Next.js: Devolver los datos básicos de la ruta de una vez
        public BaseSalesRouteDto? SalesRoute { get; set; }
    }

    public class BaseCityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
    }

    // Nuevo DTO básico para la ruta
    public class BaseSalesRouteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } // Ej. "FL-01"
    }
}