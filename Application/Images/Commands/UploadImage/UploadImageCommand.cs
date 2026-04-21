using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace eternal_api.Application.Images.Commands.UploadImage
{
    public class UploadImageCommand : IRequest<string>
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
