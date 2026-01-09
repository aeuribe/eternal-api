using MediatR;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Bills.Queries.GetBillById
{
    // 🔹 Handler: encargado de procesar una Query
    public class GetInoviceByIdHandler : IRequestHandler <GetInvoiceByIdQuery, InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        // Inyección de dependencias: el handler no accede directamente a la base de datos,
        // sino que depende de un contrato (IInvoiceRepository).
        // Así mantenemos el principio de inversión de dependencias de Clean Architecture.
        public GetInoviceByIdHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        // Método que procesa la Query.
        public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery query, CancellationToken cancellationToken)
        {
            // Buscar la factura en la base de datos mediante el repositorio.
            var invoice = await _invoiceRepository.GetByIdAsync(query.Id);

            // Si no existe ninguna factura con ese Id → devolvemos null.
            if (invoice == null) return null;

            /*
             Si existe, convertimos la entidad de dominio (Invoice) en un DTO (InvoiceDto).
             
             No debemos devolver directamente la entidad de dominio (Invoice),
                porque esta contiene información interna que no debería ser expuesta.
             
             En su lugar, usamos un DTO (Data Transfer Object):
                - Simplifica la respuesta.
                - Expone solo los datos necesarios.
                - Protege la capa de dominio de modificaciones externas.
             */

            return new InvoiceDto
            {
                Id = invoice.Id,
                Total = invoice.Total,
                CreatedAt = invoice.CreatedAt
            };
        }
    }
}
