using MediatR;

namespace eternal_api.Application.Classes.Commands.CreateClass
{
    public class CreateClassCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
