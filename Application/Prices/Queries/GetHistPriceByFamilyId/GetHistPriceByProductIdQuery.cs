using eternal_api.Application.Prices.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByProductId
{
    public class GetHistPriceByProductIdQuery : IRequest<IEnumerable<HistPriceDto>>
    {
        public Guid PresentationId { get; set; }
    }
}
