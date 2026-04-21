using eternal_api.Application.Prices.DTOs;
using eternal_api.Application.Prices.Interfaces;
using MediatR;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByProductId
{
    public class GetHistPriceByProductIdHandler : IRequestHandler<GetHistPriceByProductIdQuery, IEnumerable<HistPriceDto>>
    {
        private readonly IHistPriceRepository _histPriceRepository;

        public GetHistPriceByProductIdHandler(IHistPriceRepository histPriceRepository)
        {
            _histPriceRepository = histPriceRepository;
        }

        public async Task<IEnumerable<HistPriceDto>> Handle(GetHistPriceByProductIdQuery query, CancellationToken cancellationToken)
        {
            var prices = await _histPriceRepository.GetByPresentationIdAsync(query.PresentationId);
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
