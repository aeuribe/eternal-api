using MediatR;

namespace eternal_api.Application.Classes.Commands.UpdateClass
{
    public class UpdateClassCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
