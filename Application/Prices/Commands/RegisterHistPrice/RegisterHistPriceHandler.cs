using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Prices.Commands.RegisterHistPrice
{
    public class RegisterHistPriceHandler
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public RegisterHistPriceHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<Guid> Handle(RegisterHistPriceCommand command)
        {
            var price = new HistPrice
            {
                ProductId = command.ProductId,
                Price = command.Price,
                StartDate = command.StartDate,
                EndDate = command.EndDate
            };

            await _histPriceRepository.AddAsync(price);
            return price.Id;
        }
    }
}
