using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Bills.Queries.GetAllBills
{
    public class GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetAllInvoicesHandler(IInvoiceRepository billRepository) 
        {
            _invoiceRepository = billRepository;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery query, CancellationToken cancellationToken) 
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            return invoices.Select(b => new InvoiceDto
            {
                Id = b.Id,
                Total = b.Total,
                CreatedAt = b.CreatedAt,    
            }).ToList();
        }
    }
}
