using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Queries.GetAllInvoiceDetailsByInvoiceId
{
    public class GetAllInvoiceDetailsByInvoiceIdQuery : IRequest<IEnumerable<InvoiceDetailDto>>
    {
        public Guid InvoiceId { set; get; }
    }
}
