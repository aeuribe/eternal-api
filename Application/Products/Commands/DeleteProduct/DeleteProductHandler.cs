using eternal_api.Application.Products.Interfaces;
using MediatR;

namespace eternal_api.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null) return false;

            // Regla: No borrar si está en un planograma con órdenes
            bool hasOrders = await _productRepository.HasPlanogramsWithOrdersAsync(command.Id);
            if (hasOrders)
            {
                throw new InvalidOperationException("No se puede eliminar el producto de la base de datos porque pertenece a un planograma con órdenes registradas.");
            }

            // Borrado Físico
            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}