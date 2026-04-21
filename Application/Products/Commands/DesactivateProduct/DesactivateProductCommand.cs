using MediatR;

namespace eternal_api.Application.Products.Commands.DeactivateProduct
{
    public class DeactivateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}