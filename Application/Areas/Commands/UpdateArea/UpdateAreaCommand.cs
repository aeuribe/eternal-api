using MediatR;
namespace eternal_api.Application.Areas.Commands.UpdateArea
{
    public record UpdateAreaCommand(Guid Id, string Name) : IRequest<bool>;
}