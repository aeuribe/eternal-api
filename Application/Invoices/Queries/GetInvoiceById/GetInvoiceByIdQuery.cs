using eternal_api.Application.Common.DTOs;
using MediatR;
namespace eternal_api.Application.Bills.Queries.GetBillById
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDto>
    {
        // Un Query representa una operación de solo lectura.
        // En este caso, obtener una factura específica a partir de su Id.
        public Guid Id { get; set; }
    }
}
