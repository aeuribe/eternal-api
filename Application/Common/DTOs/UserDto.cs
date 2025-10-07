namespace eternal_api.Application.Common.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Rol { get; set; }
        public string Phone { get; set; }
        public Guid CityId { get; set; }
        public bool IsActive { get; set; }
    }

}
