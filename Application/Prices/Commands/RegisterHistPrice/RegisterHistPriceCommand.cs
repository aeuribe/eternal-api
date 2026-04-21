using MediatR;

namespace eternal_api.Application.Prices.Commands.RegisterHistPrice
{
    public class RegisterHistPriceCommand : IRequest<Guid>
    {
        public Guid PresentationId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
