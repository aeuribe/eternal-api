using eternal_api.Application.Images.Services;
using eternal_api.Application.Products.Interfaces;
using eternal_api.Application.Products.Queries.DTOs;
using MediatR;

namespace eternal_api.Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStorageService _serviceRepository;

        public GetProductByIdHandler(IProductRepository productRepository, IStorageService storageService)
        {
            _productRepository = productRepository;
            _serviceRepository = storageService;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            // 1. Primero obtenemos el producto y verificamos si existe
            // IMPORTANTE: Asegúrate de que el método GetByIdAsync en tu repositorio también tenga los 
            // .Include(p => p.Presentation).ThenInclude(pr => pr.Family).ThenInclude(f => f.Brand) etc.
            var product = await _productRepository.GetByIdAsync(query.Id);

            if (product is null)
            {
                return null;
            }

            // 2. Validamos si ImageFileName tiene contenido antes de buscar la URL
            string? imageUrl = null;
            if (!string.IsNullOrWhiteSpace(product.ImageFileName))
            {
                imageUrl = await _serviceRepository.GetFileUrlAsync(product.ImageFileName);
            }

            // 3. Mapeamos al DTO con la jerarquía completa
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code, // El código interno del producto
                ShortName = product.ShortName,
                PresentationId = product.PresentationId,
                IsActive = product.isActive,
                ImageFileName = imageUrl ?? string.Empty,
                Sku = product.Sku,

                Presentation = product.Presentation != null ? new PresentationDto
                {
                    Id = product.Presentation.Id,
                    GenericCode = product.Presentation.GenericCode,
                    Volume = product.Presentation.Volume,
                    Unit = product.Presentation.Unit,
                    IsActive = product.Presentation.IsActive,


                    Family = product.Presentation.Family != null ? new FamilyDto
                    {
                        Id = product.Presentation.Family.Id,
                        Name = product.Presentation.Family.Name,
                        FamilyCode = product.Presentation.Family.FamilyCode,
                        isActive = product.Presentation.Family.isActive,

                        Brand = product.Presentation.Family.Brand != null ? new BrandDto
                        {
                            Id = product.Presentation.Family.Brand.Id,
                            Name = product.Presentation.Family.Brand.Name
                        } : null,

                        Class = product.Presentation.Family.Class != null ? new ClassDto
                        {
                            Id = product.Presentation.Family.Class.Id,
                            Name = product.Presentation.Family.Class.Name
                        } : null
                    } : null
                } : null
            };
        }
    }
}