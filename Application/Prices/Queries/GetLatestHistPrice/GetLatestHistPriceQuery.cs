using eternal_api.Application.Prices.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries
{
    public class GetLatestHistPriceQuery : IRequest<HistPriceDto>
    {
        public Guid PresentationId { get; set; }
    }
}
