using MediatR;

namespace eternal_api.Application.Areas.Commands.CreateArea
{
    public class CreateAreaCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
