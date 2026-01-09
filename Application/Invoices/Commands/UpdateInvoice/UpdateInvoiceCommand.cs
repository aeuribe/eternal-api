using MediatR;
namespace eternal_api.Application.Bills.Commands.UpdateBill
{
    public class UpdateInvoiceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
