using MediatR;

namespace eternal_api.Application.Classes.Commands.DeleteClass
{
    public class DeleteClassCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
