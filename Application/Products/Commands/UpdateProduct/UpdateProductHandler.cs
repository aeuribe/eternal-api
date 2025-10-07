using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Products.Commands.CreateProduct;

namespace eternal_api.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null) return false;

            product.Update(command.Name, command.Category, command.SKU);
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
