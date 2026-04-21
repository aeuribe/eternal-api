using eternal_api.Application.Images.Commands.UploadImage;
//using eternal_api.Application.Images.Commands.DeleteImage;
using eternal_api.Application.Images.Queries.GetImageUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class StorageEndpoints
    {
        public static void MapStorageEndpoints(this IEndpointRouteBuilder app)
        {
            // -------------------------
            // UPLOAD
            // -------------------------
            app.MapPost("/upload", async (IFormFile file, IMediator mediator) =>
            {
                var command = new UploadImageCommand { File = file };
                var result = await mediator.Send(command);
                return Results.Created($"/images/{result}", result);
            })
            .DisableAntiforgery();


            // -------------------------
            // GET URL
            // -------------------------
            app.MapGet("/url/{fileName}", async (string fileName, IMediator mediator) =>
            {
                var query = new GetImageUrlQuery { FileName = fileName };
                var url = await mediator.Send(query);

                return Results.Ok(new
                {
                    FileName = fileName,
                    Url = url
                });
            });

            // -------------------------
            // DELETE
            // -------------------------
           // app.MapDelete("/storage/{fileName}", async (string fileName, IMediator mediator) =>
           // {
            //    var command = new DeleteImageCommand { FileName = fileName };
             //   var success = await mediator.Send(command);

//                return success ? Results.NoContent() : Results.NotFound();
  //          });
        }
    }
}