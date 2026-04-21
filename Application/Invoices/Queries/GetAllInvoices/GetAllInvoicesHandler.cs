using eternal_api.Application.Bills.Queries.GetAllBills;
using eternal_api.Application.Images.Services;
using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Invoices.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IStorageService _storageService;
        public GetAllInvoicesHandler(IInvoiceRepository invoiceRepository, IStorageService storageService, IPOProvider POProvider)
        {
            _invoiceRepository = invoiceRepository;
            _storageService = storageService;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery query, CancellationToken cancellationToken)
        {
            // 1. Traemos las facturas. Asegúrate de que el Repo incluya los detalles (Include)
            var invoices = await _invoiceRepository.GetAllAsync();

            // 2. Proyectamos a tareas para obtener las URLs de S3
            var invoiceTasks = invoices.Select(async b =>
            {
                string? podImageUrl = null;
                if (!string.IsNullOrWhiteSpace(b.POD))
                {
                    podImageUrl = await _storageService.GetFileUrlAsync(b.POD);
                }


                // Dentro del Select del Handler:
                return new InvoiceDto
                {
                    Id = b.Id,
                    OrderId = b.OrderId,
                    Total = b.Total,
                    CreatedAt = b.CreatedAt,
                    PodImageUrl = podImageUrl,
                    InvoiceNumber = b.InvoiceNumber,
                    Items = b.invoiceDetails?.Select(d => new InvoiceDto.InvoiceDetailDto // Referencia a la clase anidada
                    {
                        InvoiceDetailId = d.Id,
                        InvoiceId = d.InvoiceId,
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        Subtotal = d.Subtotal // Cast si en la DB aún es float, pero mejor cámbialo a decimal
                    }).ToList() ?? new List<InvoiceDto.InvoiceDetailDto>()
                };
            });

            // 4. Ejecutamos las tareas de generación de URL en paralelo
            return await Task.WhenAll(invoiceTasks);
        }
    }
}