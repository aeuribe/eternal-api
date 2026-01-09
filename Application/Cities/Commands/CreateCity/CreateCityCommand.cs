using MediatR;

namespace eternal_api.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
