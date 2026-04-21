using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;

        // Ahora recibe el Enum
        public StateUsEnum State { get; set; }
    }
}