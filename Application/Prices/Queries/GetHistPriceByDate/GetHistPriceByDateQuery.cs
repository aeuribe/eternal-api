using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByDate
{
    public class GetHistPriceByDateQuery : IRequest<HistPriceDto>
    {
        public Guid ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
