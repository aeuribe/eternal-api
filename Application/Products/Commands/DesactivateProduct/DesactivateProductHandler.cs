using eternal_api.Application.Products.Interfaces;
using MediatR;

namespace eternal_api.Application.Products.Commands.DeactivateProduct
{
    public class DeactivateProductHandler : IRequestHandler<DeactivateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeactivateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeactivateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null) return false;

            // Regla: No desactivar si está en un planograma ACTIVO
            bool isInActivePlanogram = await _productRepository.IsInActivePlanogramAsync(command.Id);
            if (isInActivePlanogram)
            {
                throw new InvalidOperationException("No se puede desactivar el producto porque está siendo utilizado en un planograma activo.");
            }

            // Borrado Lógico / Desactivación
            product.Deactivate();
            await _productRepository.UpdateAsync(product);

            return true;
        }
    }
}