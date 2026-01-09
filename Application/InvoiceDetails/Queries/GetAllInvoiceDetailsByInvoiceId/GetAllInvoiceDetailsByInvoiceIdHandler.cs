using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Queries.GetAllInvoiceDetailsByInvoiceId
{
    public class GetAllInvoiceDetailsByInvoiceIdHandler : IRequestHandler<GetAllInvoiceDetailsByInvoiceIdQuery, IEnumerable<InvoiceDetailDto>>
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public GetAllInvoiceDetailsByInvoiceIdHandler(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }
        
        public async Task<IEnumerable<InvoiceDetailDto>> Handle(GetAllInvoiceDetailsByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var invoiceDetails = await _invoiceDetailRepository.GetAllInvoiceDetailsByInvoiceIdAsync(query.InvoiceId);

            return invoiceDetails.Select(id => new InvoiceDetailDto
            {
                Id = id.Id,
                InvoiceId = id.InvoiceId,
                ProductId = id.ProductId,
                Quantity = id.Quantity,
                SubTotal = id.Subtotal
            }).ToList();
        }
    }
}
