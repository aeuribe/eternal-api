using MediatR;

namespace eternal_api.Application.Stores.Commands.DesactivateStore
{
    public class DesactivateStoreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
