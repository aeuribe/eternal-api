using eternal_api.Application.Images.Services;
using MediatR;

namespace eternal_api.Application.Images.Queries.GetImageUrl
{
    public class GetImageUrlHandler : IRequestHandler<GetImageUrlQuery, string>
    {
        public readonly IStorageService _storageService;

        public GetImageUrlHandler(IStorageService storageService) 
        {
            _storageService = storageService;
        }

        public async Task<string> Handle(GetImageUrlQuery query, CancellationToken cancellationToken)
        {
            // Obtiene la URL (pública o prefirmada) desde el servicio
            var url = await _storageService.GetFileUrlAsync(query.FileName);
            return url;
        }
    }
}
