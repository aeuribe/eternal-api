using eternal_api.Domain.Enums;
using MediatR;

namespace eternal_api.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Actualizado al Enum
        public StateUsEnum State { get; set; }
    }
}