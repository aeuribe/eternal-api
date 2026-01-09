using MediatR;

namespace eternal_api.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
