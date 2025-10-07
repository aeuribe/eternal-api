using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Products.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetProductsByCategoryQuery query)
        {
            var products = await _productRepository.GetByCategoryAsync(query.Category);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                SKU = p.SKU,
                isActive = p.isActive
            }).ToList();
        }
    }
}
