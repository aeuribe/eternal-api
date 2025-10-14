namespace eternal_api.Application.Common.DTOs
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid CityId { get; set; }
    }
}
