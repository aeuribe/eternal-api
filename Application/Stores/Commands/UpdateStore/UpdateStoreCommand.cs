using MediatR;

namespace eternal_api.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string StoreNumber { get; set; } = string.Empty;
        public string ZoneNumber { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Street { get; set; } = string.Empty;
        public bool HasPlanogram { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
    }
}