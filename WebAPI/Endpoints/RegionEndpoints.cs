using eternal_api.Application.Regions.Commands.CreateRegion;
using eternal_api.Application.Regions.Commands.DeleteRegion;
using eternal_api.Application.Regions.Commands.UpdateRegion;
using eternal_api.Application.Regions.Queries.GetRegionById;
using eternal_api.Application.Regions.Queries.GetRegions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class RegionEndpoints
    {
        public static void MapRegionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/regions").WithTags("Regions");

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateRegionCommand command) =>
            {
                var id = await mediator.Send(command);
                return Results.Ok(new { Id = id });
            });

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetRegionsQuery());
                return Results.Ok(result);
            });

            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetRegionByIdQuery(id));
                return result != null ? Results.Ok(result) : Results.NotFound("Región no encontrada.");
            });

            group.MapPut("/{id:guid}", async (Guid id, IMediator mediator, [FromBody] UpdateRegionCommand command) =>
            {
                if (id != command.Id) return Results.BadRequest("El ID de la ruta no coincide con el del cuerpo.");

                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound("Región no encontrada.");
            });

            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteRegionCommand(id));

                if (!result.Succeeded)
                {
                    return result.ErrorMessage == "Región no encontrada."
                        ? Results.NotFound(result.ErrorMessage)
                        : Results.BadRequest(new { Message = result.ErrorMessage });
                }

                return Results.NoContent();
            });
        }
    }
}