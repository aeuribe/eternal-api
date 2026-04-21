using eternal_api.Application.Prices.DTOs;
using MediatR;

namespace eternal_api.Application.Prices.Queries.GetHistPriceByDate
{
    public class GetHistPriceByDateQuery : IRequest<HistPriceDto>
    {
        public Guid PresentationId { get; set; }
        public DateTime Date { get; set; }
    }
}
