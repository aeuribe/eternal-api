using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Queries.GetInvoiceDetailById
{
    public class GetInvoiceDetailByIdQuery : IRequest<InvoiceDetailDto>
    {
        public Guid Id { set; get; }
    }
}
