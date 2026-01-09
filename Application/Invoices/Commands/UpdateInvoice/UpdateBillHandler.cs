using MediatR;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Bills.Commands.UpdateBill
{
    public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public UpdateInvoiceHandler( IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<bool> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var bill = await _invoiceRepository.GetByIdAsync(command.Id);
            if (bill is null) return false;

            bill.Update(command.Total, command.OrderId);
            await _invoiceRepository.UpdateAsync(bill);
            return true;
        }
    }
}
