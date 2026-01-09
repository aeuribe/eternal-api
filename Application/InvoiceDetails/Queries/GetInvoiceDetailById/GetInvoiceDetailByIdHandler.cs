using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Queries.GetInvoiceDetailById
{
    public class GetInvoiceDetailByIdHandler : IRequestHandler<GetInvoiceDetailByIdQuery, InvoiceDetailDto>
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public GetInvoiceDetailByIdHandler(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<InvoiceDetailDto> Handle(GetInvoiceDetailByIdQuery query, CancellationToken cancellationToken)
        {
            if (!await _invoiceDetailRepository.ExistsAsync(query.Id))
            {
                throw new NotFoundException("InvoiceDetail", query.Id);
            }
            var invoiceDetail = await _invoiceDetailRepository.GetByIdAsync(query.Id);
            
            return new InvoiceDetailDto
            {
                Id = invoiceDetail.Id,
                InvoiceId = invoiceDetail.InvoiceId,
                ProductId = invoiceDetail.ProductId,
                Quantity = invoiceDetail.Quantity,
                SubTotal = invoiceDetail.Subtotal
            };
        }
    }
}
