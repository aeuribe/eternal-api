using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
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

            product.Deactivate();
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
