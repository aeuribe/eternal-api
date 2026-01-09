using MediatR;

namespace eternal_api.Application.Bills.Commands.CreateBill
{
    // Command: representa una "intención de acción"
    // Es el objeto que trae los datos mínimos que el cliente envía
    // para pedirle al sistema que cree una factura (Invoice).
    public class CreateInvoiceCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }

    }
}
