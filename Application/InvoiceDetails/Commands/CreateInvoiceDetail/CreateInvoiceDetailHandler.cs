using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Commands.CreateInvoiceDetail
{
    public class CreateInvoiceDetailHandler : IRequestHandler<CreateInvoiceDetailCommand, Guid>
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public CreateInvoiceDetailHandler(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<Guid> Handle(CreateInvoiceDetailCommand command, CancellationToken cancellationToken)
        {
            var invoiceDetail = new InvoiceDetail(command.InvoiceId, command.ProductId, command.Quantity, command.Subtotal);
            await _invoiceDetailRepository.AddAsync(invoiceDetail);
            return invoiceDetail.Id;
        }
    }
}
