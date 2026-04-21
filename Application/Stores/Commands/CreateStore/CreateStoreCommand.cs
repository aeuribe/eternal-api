using MediatR;

namespace eternal_api.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<Guid>
    {
        public string StoreNumber { get; set; } = string.Empty;
        public string ZoneNumber { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string? Name { get; set; } // Es nullable porque en tu entidad le pusiste el "?"
        public string Street { get; set; } = string.Empty;
        public bool HasPlanogram { get; set; }
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
    }
}