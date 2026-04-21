using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using eternal_api.Application.Images.Services;
using Microsoft.Extensions.Configuration;

namespace eternal_api.Infrastructure.Services
{
    public class S3StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3StorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"]
                ?? throw new ArgumentNullException("AWS:BucketName no está configurado");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("El archivo no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("El nombre del archivo es inválido.");

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = _bucketName,
                ContentType = contentType,
                // CannedACL = S3CannedACL.PublicRead
            };

            using var fileTransferUtility = new TransferUtility(_s3Client);

            try
            {
                await fileTransferUtility.UploadAsync(uploadRequest);
            }
            catch (AmazonS3Exception ex)
            {
                throw new InvalidOperationException($"Error subiendo archivo a S3: {ex.Message}", ex);
            }

            return fileName;
        }

        public async Task<string?> GetFileUrlAsync(string fileName)
        {
            // 1. Si el nombre es nulo o vacío, devolvemos null silenciosamente
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            try
            {
                // 2. Verificamos si existe en S3
                await _s3Client.GetObjectMetadataAsync(_bucketName, fileName);
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // 3. EN LUGAR DE THROW: Devolvemos null. 
                // Esto permite que el resto del listado de productos cargue sin problemas.
                return null;
            }
            catch (Exception)
            {
                // Captura cualquier otro error (red, permisos) y evita que el API muera
                return null;
            }

            // 4. Si llegamos aquí, el archivo existe. Generamos la URL.
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            return _s3Client.GetPreSignedURL(request);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            // Si no hay nombre válido, no hacemos nada
            if (string.IsNullOrWhiteSpace(fileName)) return;

            try
            {
                var deleteRequest = new Amazon.S3.Model.DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
            }
            catch (Exception ex)
            {
                // Aquí puedes hacer un log del error si quieres. 
                // No lanzamos la excepción para evitar que el proceso de guardado 
                // en la base de datos falle solo porque S3 tuvo un problema menor.
                Console.WriteLine($"Error eliminando el archivo {fileName} de S3: {ex.Message}");
            }
        }
    }
}