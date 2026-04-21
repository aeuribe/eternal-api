using eternal_api.Application.Districts.Commands.CreateDistrict;
using eternal_api.Application.Districts.Commands.DeleteDistrict;
using eternal_api.Application.Districts.Commands.UpdateDistrict;
using eternal_api.Application.Districts.Queries.GetDistrictById;
using eternal_api.Application.Districts.Queries.GetDistricts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class DistrictEndpoints
    {
        public static void MapDistrictEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/districts").WithTags("Districts");

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateDistrictCommand command) =>
            {
                var id = await mediator.Send(command);
                return Results.Ok(new { Id = id });
            });

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetDistrictsQuery());
                return Results.Ok(result);
            });

            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetDistrictByIdQuery(id));
                return result != null ? Results.Ok(result) : Results.NotFound("Distrito no encontrado.");
            });

            group.MapPut("/{id:guid}", async (Guid id, IMediator mediator, [FromBody] UpdateDistrictCommand command) =>
            {
                if (id != command.Id) return Results.BadRequest("El ID de la ruta no coincide con el del cuerpo.");

                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound("Distrito no encontrado.");
            });

            group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteDistrictCommand(id));

                if (!result.Succeeded)
                {
                    return result.ErrorMessage == "Distrito no encontrado."
                        ? Results.NotFound(result.ErrorMessage)
                        : Results.BadRequest(new { Message = result.ErrorMessage });
                }

                return Results.NoContent();
            });
        }
    }
}