using eternal_api.Application.Images.Services;
using eternal_api.Application.Products.Interfaces;
using MediatR;

namespace eternal_api.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStorageService _storageService; // 1. Agregamos el servicio

        public UpdateProductHandler(IProductRepository productRepository, IStorageService storageService)
        {
            _productRepository = productRepository;
            _storageService = storageService;
        }

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null) return false;

            // 2. Verificamos si el frontend está enviando una NUEVA imagen
            if (!string.IsNullOrWhiteSpace(command.ImageFileName))
            {
                // 3. Si el producto YA tenía una imagen y es diferente a la nueva, la borramos de S3
                if (!string.IsNullOrWhiteSpace(product.ImageFileName) && product.ImageFileName != command.ImageFileName)
                {
                    await _storageService.DeleteFileAsync(product.ImageFileName);
                }

                // 4. Asignamos la nueva imagen a la entidad
                product.ImageFileName = command.ImageFileName;
            }
            // (Si command.ImageFileName viene nulo o vacío, simplemente ignora esto y conserva la imagen vieja)

            // 5. Actualizamos los demás campos del dominio
            product.Update(command.Name, command.ShortName, command.Code, command.PresentationId, command.Sku);

            await _productRepository.UpdateAsync(product);

            return true;
        }
    }
}