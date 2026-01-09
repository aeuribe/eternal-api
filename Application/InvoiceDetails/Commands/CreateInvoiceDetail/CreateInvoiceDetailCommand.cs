using MediatR;

namespace eternal_api.Application.InvoiceDetails.Commands.CreateInvoiceDetail
{
    public class CreateInvoiceDetailCommand : IRequest<Guid>
    {
        public Guid InvoiceId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public float Subtotal { set; get; }
    }
}
