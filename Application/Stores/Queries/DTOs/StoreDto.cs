namespace eternal_api.Application.Stores.Queries.DTOs
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string StoreNumber { get; set; } = string.Empty;
        public string ZoneNumber { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Street { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool HasPlanogram { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
    }
}