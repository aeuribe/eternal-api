using eternal_api.Application.Products.Interfaces;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // 1. Crear la instancia base del producto
            var product = new Product(command.Name, command.ShortName, command.Code, command.PresentationId, command.Sku);

            // 2. Asignación condicional: Solo si ImageFileName tiene contenido real
            if (!string.IsNullOrWhiteSpace(command.ImageFileName))
            {
                product.ImageFileName = command.ImageFileName;
            }

            // 3. Guardar en el repositorio
            await _productRepository.AddAsync(product);

            return product.Id;
        }
    }
}
