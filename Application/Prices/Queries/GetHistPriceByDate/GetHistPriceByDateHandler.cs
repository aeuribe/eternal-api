using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Prices.Queries.GetHistPriceByDate;
using MediatR;

namespace eternal_api.Application.HistPrices.Queries.GetHistPriceByDate
{
    public class GetHistPriceByDateHandler : IRequestHandler<GetHistPriceByDateQuery, HistPriceDto>
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public GetHistPriceByDateHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<HistPriceDto?> Handle(GetHistPriceByDateQuery query, CancellationToken cancellationToken)
        {
            var price = await _histPriceRepository.GetByDateAsync(query.ProductId, query.Date);

            if (price == null)
                return null;

            return new HistPriceDto
            {
                Id = price.Id,
                Price = price.Price,
                StartDate = price.StartDate,
                EndDate = price.EndDate
            };
        }
    }
}
