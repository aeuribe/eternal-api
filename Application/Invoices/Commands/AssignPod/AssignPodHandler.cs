using MediatR;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Bills.Commands.AssignPod
{
    public class AssignPodHandler : IRequestHandler<AssignPodCommand, Guid>
    {
        public readonly IInvoiceRepository _invoiceRepository;

        public AssignPodHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Guid> Handle(AssignPodCommand command, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(command.Id);
            if (invoice == null)
                throw new NotFoundException("Invoice", command.Id);

            invoice.AssignPOD(command.ImageUrl);
            await _invoiceRepository.UpdateAsync(invoice);

            return invoice.Id;
        }
    }
}
