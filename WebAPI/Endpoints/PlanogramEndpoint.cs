
using eternal_api.Application.Planograms.Commands.CreatePlanogram;
using eternal_api.Application.Planograms.Queries.GetPlanogramById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eternal_api.Application.Planograms.Queries.GetAllPlanograms;
using eternal_api.Application.Planograms.Commands.UpdatePlanogram;
using eternal_api.Application.Planograms.Commands.DesactivatePlanogram;

namespace eternal_api.WebAPI.Endpoints
{
    public static class PlanogramEndpoint
    {
        public static IEndpointRouteBuilder MapPlanogramEndpoints(this IEndpointRouteBuilder app)
        {   
            // Crear planograma
            app.MapPost("/planograms", async (CreatePlanogramCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            // Obtener planograma por Id
            app.MapGet("/planograms/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetPlanogramByIdQuery { Id = id };
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Obtener todos los planogramas
            app.MapGet("/planograms", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllPlanogramsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            // Actualizar
            app.MapPut("/planograms/{id:guid}", async (Guid id, UpdateStatusPlanogramCommand command, IMediator mediator) =>
            {
                // Aseguras que el id de la ruta prevalezca sobre el body
                command.Id = id;

                var result = await mediator.Send(command);

                return result
                    ? Results.NoContent()   // 204 si se actualizó correctamente
                    : Results.NotFound();   // 404 si no existe el recurso
            });

            // Desactivar
            app.MapPut("/planograms/desactivate/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DesactivatePlanogramCommand() { Id = id };
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });


            return app;
        }
    }
}
