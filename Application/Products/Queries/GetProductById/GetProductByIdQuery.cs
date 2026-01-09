using eternal_api.Application.Common.DTOs;
using MediatR;

namespace eternal_api.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        // Un Query representa una operación de solo lectura.
        // En este caso, obtener una factura específica a partir de su Id.
        public Guid Id { get; set; }
    }
}
