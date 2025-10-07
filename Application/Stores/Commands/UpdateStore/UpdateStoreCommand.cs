namespace eternal_api.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }

}
