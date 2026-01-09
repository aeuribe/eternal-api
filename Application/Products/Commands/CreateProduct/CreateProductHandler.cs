using eternal_api.Application.Common.Interfaces;
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
            // Crear la entidad con el constructor
            var product = new Product(command.Name, command.Category, command.SKU)
            {
                isActive = command.isActive
            };

            // Guardar en el repositorio
            await _productRepository.AddAsync(product);

            /*
             El ID se crea en el constructor con el uso de Guid,
             por eso existe un Id que se puede retornar sin esperar
             la respuesta de la inserción en el repositorio
            */
            return product.Id;
        }
    }
}
