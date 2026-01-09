using eternal_api.Application.Common.Interfaces;
using eternal_api.Infraestructure.Repositories;
using MediatR;

namespace eternal_api.Application.InvoiceDetails.Commands.UpdateInvoiceDetail
{
    public class UpdateInvoiceDetailHandler : IRequestHandler<UpdateInvoiceDetailCommand, bool>
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        public UpdateInvoiceDetailHandler(IInvoiceDetailRepository invoiceDetailRepository)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
        }

        public async Task<bool> Handle(UpdateInvoiceDetailCommand command, CancellationToken cancellationToken)
        {
            var invoiceDetail = await _invoiceDetailRepository.GetByIdAsync(command.Id);
            if (invoiceDetail == null) return false;

            invoiceDetail.Update(command.InvoiceId, command.ProductId, command.Quantity, command.Subtotal);
            await _invoiceDetailRepository.UpdateAsync(invoiceDetail);
            return true;
        }
    }
}
