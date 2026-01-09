using MediatR;
using eternal_api.Application.Bills.Commands.CreateBill;
using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.invoices.Commands.Createinvoice
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository, IOrderRepository orderRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            if (!await _orderRepository.ExistsAsync(command.OrderId))
                throw new NotFoundException("Order", command.OrderId);
                
            var invoice = new Invoice(command.OrderId, command.Total);

            await _invoiceRepository.AddAsync(invoice);

            /*
             El ID se crea en el constructor con el uso de Guid,
             por eso existe un Id que se puede retornar sin esperar
             la respuesta de la inserción en el repositorio
            */
            return invoice.Id;
        }
    }
}
