using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByProductId
{
    public class GetHistPriceByProductIdQuery : IRequest<IEnumerable<HistPriceDto>>
    {
        public Guid ProductId { get; set; }
    }
}
