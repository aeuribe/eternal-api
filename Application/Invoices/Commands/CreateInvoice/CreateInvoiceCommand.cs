using MediatR;

namespace eternal_api.Application.Bills.Commands.CreateBill
{
    // Command: representa una "intención de acción"
    // Es el objeto que trae los datos mínimos que el cliente envía
    // para pedirle al sistema que cree una factura (Invoice).
    public class CreateInvoiceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public string? POD { get; set; }


        public List<InvoiceItemDto> Items { get; set; } = new();

        public class InvoiceItemDto
        {
            public Guid InvoiceDetailId { get; set; } // <-- El ID del detalle generado por el Frontend
            public Guid InvoiceId { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Subtotal { get; set; }
        }

    }
}
