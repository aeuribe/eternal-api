using eternal_api.Application.Areas.Commands.CreateArea;
using eternal_api.Application.Areas.Commands.DeleteArea;
using eternal_api.Application.Areas.Commands.UpdateArea;
using eternal_api.Application.Areas.Queries.GetAreaById;
using eternal_api.Application.Areas.Queries.GetAreas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class AreaEndpoints
    {
        public static void MapAreaEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupamos la ruta base y la etiqueta para Swagger
            var group = routes.MapGroup("/areas").WithTags("Areas");

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateAreaCommand command) =>
            {
                var id = await mediator.Send(command);
                return Results.Ok(new { Id = id });
            });

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAreasQuery());
                return Results.Ok(result);
            });

            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAreaByIdQuery { Id = id });
                return result != null ? Results.Ok(result) : Results.NotFound("Área no encontrada.");
            });

            group.MapPut("/{id:guid}", async (Guid id, IMediator mediator, [FromBody] UpdateAreaCommand command) =>
            {
                if (id != command.Id) return Results.BadRequest("El ID de la ruta no coincide con el del cuerpo.");

                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound("Área no encontrada.");
            });

            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteAreaCommand(id));

                if (!result.Succeeded)
                {
                    return result.ErrorMessage == "Área no encontrada."
                        ? Results.NotFound(result.ErrorMessage)
                        : Results.BadRequest(new { Message = result.ErrorMessage });
                }

                return Results.NoContent();
            });
        }
    }
}