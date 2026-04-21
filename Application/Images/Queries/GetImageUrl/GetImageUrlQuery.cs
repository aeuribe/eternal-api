using MediatR;

namespace eternal_api.Application.Images.Queries.GetImageUrl
{
    public class GetImageUrlQuery : IRequest<string>
    {
        public string? FileName { get; set; }
    }
}
