using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Prices.Queries;

namespace eternal_api.Application.HistPrices.Queries.GetLatestHistPrice
{
    public class GetLatestHistPriceHandler
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public GetLatestHistPriceHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<HistPriceDto?> Handle(GetLatestHistPriceQuery query)
        {
            var price = await _histPriceRepository.GetLatestAsync(query.ProductId);
            return price is null ? null : new HistPriceDto
            {
                Id = price.Id,
                Price = price.Price,
                StartDate = price.StartDate,
                EndDate = price.EndDate
            };
        }
    }
}
