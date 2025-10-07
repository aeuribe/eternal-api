using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByProductId
{
    public class GetHistPriceByProductIdHandler
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public GetHistPriceByProductIdHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<List<HistPriceDto>> Handle(GetHistPriceByProductIdQuery query)
        {
            var prices = await _histPriceRepository.GetByProductIdAsync(query.ProductId);
            return prices.Select(p => new HistPriceDto
            {
                Id = p.Id,
                Price = p.Price,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            }).ToList();
        }
    }
}
