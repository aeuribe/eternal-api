using MediatR;

namespace eternal_api.Application.InvoiceDetails.Commands.UpdateInvoiceDetail
{
    public class UpdateInvoiceDetailCommand : IRequest<bool>
    {
        public Guid Id { set; get; }
        public Guid InvoiceId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public float Subtotal { set; get; }
    }
}
