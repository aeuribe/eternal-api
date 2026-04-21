namespace eternal_api.Application.Images.Services
{
    public interface IStorageService
    {
        // Sube un archivo y devuelve al clave (filename) o la URL pública
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);

        // Genera una URL prefirmada (Útil si el bucket es privado) o devuelve la pública
        Task<string> GetFileUrlAsync(string fileName);

        // Elimina el archivo del bucket
        Task DeleteFileAsync(string fileName);
    }
}
