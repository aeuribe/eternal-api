namespace eternal_api.Application.SalesRoutes.DTOs
{
    public class SalesRouteDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } // Ej: "FL-01"
        public string Name { get; set; } // Ej: "Miami North"
        public Guid CityId { get; set; }
        public bool IsActive { get; set; } // Para el toggle switch en Next.js

        // LA ESTRELLA: El campo calculado para bloquear inputs y botones
        public bool HasOrders { get; set; }
    }
}