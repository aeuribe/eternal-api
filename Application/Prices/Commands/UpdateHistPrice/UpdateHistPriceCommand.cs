using MediatR;

namespace eternal_api.Application.Prices.Commands.UpdateHistPrice
{
    public class UpdateHistPriceCommand : IRequest<Guid>
    {
        public Guid PresentationId { set; get; }
        public decimal Price { set; get; }

    }
}
