namespace eternal_api.Domain.Entities
{
    public class POD
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }

        public POD(string imageUrl)
        {
            Id = Guid.NewGuid();
            ImageUrl = imageUrl;
        }
    }
}
