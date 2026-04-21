using eternal_api.Application.Images.Services;
using eternal_api.Application.Products.Interfaces;
using eternal_api.Application.Products.Queries.DTOs; // Solo usamos los DTOs de Products
using MediatR;

namespace eternal_api.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStorageService _storageService;

        public GetAllProductsHandler(IProductRepository productRepository, IStorageService storageService)
        {
            _productRepository = productRepository;
            _storageService = storageService;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var productDtosTasks = products.Select(async p =>
            {
                string? imageUrl = null;

                if (!string.IsNullOrWhiteSpace(p.ImageFileName))
                {
                    imageUrl = await _storageService.GetFileUrlAsync(p.ImageFileName);
                }

                return new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    PresentationId = p.PresentationId,
                    IsActive = p.isActive,
                    ImageFileName = imageUrl ?? string.Empty,
                    ShortName = p.ShortName,
                    Sku = p.Sku,

                    Presentation = p.Presentation != null ? new PresentationDto
                    {
                        Id = p.Presentation.Id,
                        GenericCode = p.Presentation.GenericCode,
                        Volume = p.Presentation.Volume,
                        Unit = p.Presentation.Unit,
                        IsActive = p.Presentation.IsActive,

                        Family = p.Presentation.Family != null ? new FamilyDto
                        {
                            Id = p.Presentation.Family.Id,
                            Name = p.Presentation.Family.Name,
                            FamilyCode = p.Presentation.Family.FamilyCode,
                            isActive = p.Presentation.Family.isActive,

                            Brand = p.Presentation.Family.Brand != null ? new BrandDto
                            {
                                Id = p.Presentation.Family.Brand.Id,
                                Name = p.Presentation.Family.Brand.Name
                            } : null,

                            Class = p.Presentation.Family.Class != null ? new ClassDto
                            {
                                Id = p.Presentation.Family.Class.Id,
                                Name = p.Presentation.Family.Class.Name
                            } : null
                        } : null
                    } : null
                };
            });

            var result = await Task.WhenAll(productDtosTasks);

            return result;
        }
    }
}