using eternal_api.Application.Images.Services;
using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Invoices.Queries.DTOs;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Bills.Queries.GetBillById
{
    // 🔹 Handler: encargado de procesar una Query
    public class GetInoviceByIdHandler : IRequestHandler <GetInvoiceByIdQuery, InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IStorageService _storageService;
        private readonly IPOProvider _POProvider;

        // Inyección de dependencias: el handler no accede directamente a la base de datos,
        // sino que depende de un contrato (IInvoiceRepository).
        // Así mantenemos el principio de inversión de dependencias de Clean Architecture.
        public GetInoviceByIdHandler(IInvoiceRepository invoiceRepository, IStorageService storageService, IPOProvider POProvider)
        {
            _invoiceRepository = invoiceRepository;
            _storageService = storageService;
            _POProvider = POProvider;
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

            // 2. Validamos si ImageFileName tiene contenido antes de buscar la URL
            string? imageUrl = null;
            if (!string.IsNullOrWhiteSpace(invoice.POD))
            {
                imageUrl = await _storageService.GetFileUrlAsync(invoice.POD);
            }

            return new InvoiceDto
            {
                Id = invoice.Id,
                Total = invoice.Total,
                CreatedAt = invoice.CreatedAt,
                OrderId = invoice.OrderId,
                PodImageUrl = imageUrl,
                InvoiceNumber = invoice.InvoiceNumber,
                Items = invoice.invoiceDetails?.Select(d => new InvoiceDto.InvoiceDetailDto // Referencia a la clase anidada
                {
                    InvoiceDetailId = d.Id,
                    InvoiceId = d.InvoiceId,
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    Subtotal = d.Subtotal // Cast si en la DB aún es float, pero mejor cámbialo a decimal
                }).ToList() ?? new List<InvoiceDto.InvoiceDetailDto>()
            }; 
        }
    }
}
