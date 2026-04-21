namespace eternal_api.Application.Cities.Queries.DTOs
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StatePrefix { get; set; } = string.Empty;
        public string StateFullName { get; set; } = string.Empty;
        public string Country { get; set; } = "USA"; // Hardcoded ya que operan en USA
    }
}