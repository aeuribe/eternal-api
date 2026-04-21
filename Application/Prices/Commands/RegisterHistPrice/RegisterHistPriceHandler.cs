using eternal_api.Application.Prices.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Prices.Commands.RegisterHistPrice
{
    public class RegisterHistPriceHandler : IRequestHandler<RegisterHistPriceCommand, Guid>
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public RegisterHistPriceHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<Guid> Handle(RegisterHistPriceCommand command, CancellationToken cancellationToken)
        {
            var price = new HistPrice
            {
                PresentationId = command.PresentationId,
                Price = command.Price,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            };

            await _histPriceRepository.AddAsync(price);
            return price.Id;
        }
    }
}
