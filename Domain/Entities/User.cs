namespace eternal_api.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rol { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = true;

        public string IdentityUserId { get; set; }

        // 1. EL CAMBIO VITAL: Guid? para hacerlo opcional
        public Guid? SalesRouteId { get; set; }
        public SalesRoute? SalesRoute { get; set; }

        private User() { }

        public User(string name, string lastName, string rol, string phone, string identityUserId)
        {
            Id = Guid.NewGuid();
            IsActive = true;
            Name = name;
            LastName = lastName;
            Rol = rol;
            Phone = phone;
            IdentityUserId = identityUserId;
            // No asignamos SalesRouteId aquí, nace en null
        }

        public void Update(string name, string lastName, string rol, string phone)
        {
            Name = name;
            LastName = lastName;
            Rol = rol;
            Phone = phone;
        }

        public void Desactivate()
        {
            IsActive = !IsActive;
        }

        // 2. Método exclusivo para cuando hagas el Update de la ruta
        public void AssignRoute(Guid routeId)
        {
            SalesRouteId = routeId;
        }

        public void RemoveRoute()
        {
            SalesRouteId = null;
        }
    }
}