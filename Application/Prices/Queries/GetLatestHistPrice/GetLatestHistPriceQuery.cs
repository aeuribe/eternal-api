using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries
{
    public class GetLatestHistPriceQuery : IRequest<HistPriceDto>
    {
        public Guid ProductId { get; set; }
    }
}
