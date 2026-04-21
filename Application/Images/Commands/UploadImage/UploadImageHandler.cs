using eternal_api.Application.Images.Services;
using MediatR;

namespace eternal_api.Application.Images.Commands.UploadImage
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommand, string>
    {
        public readonly IStorageService _storageService;
        public UploadImageHandler(IStorageService storageService) 
        {
            _storageService = storageService;
        }
        public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("Archivo no válido.");

            var uniqueFileName = $"{Guid.NewGuid()}_{request.File.FileName}";
            using var stream = request.File.OpenReadStream();

            // Subimos a S3. Esto nos devuelve el Key (uniqueFileName)
            var fileKey = await _storageService.UploadFileAsync(stream, uniqueFileName, request.File.ContentType);

            // AQUÍ: Llamarías a tu Repositorio para guardar 'fileKey' en PostgreSQL
            // ej: await _imageMetadataRepository.SaveAsync(new Image { Key = fileKey });

            return fileKey;
        }
    }
}
