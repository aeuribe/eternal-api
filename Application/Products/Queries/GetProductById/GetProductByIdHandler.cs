using eternal_api.Application.Bills.Queries.GetBillById;
using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery query)
        {
            var product = await _productRepository.GetByIdAsync(query.Id);
            return product is null ? null : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                SKU = product.SKU,
                isActive = product.isActive
            };
        }
    }
}
