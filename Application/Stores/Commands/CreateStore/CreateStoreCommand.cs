namespace eternal_api.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Guid CityId { get; set; }
    }

}
