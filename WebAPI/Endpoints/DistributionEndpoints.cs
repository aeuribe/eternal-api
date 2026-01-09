using eternal_api.Application.Distributions.Commands.AddDistribution;
using eternal_api.Application.Distributions.Commands.UpdateDistribution;
using eternal_api.Application.Distributions.Commands.DeleteDistribution;
//using eternal_api.Application.Distributions.Queries.GetDistributionById;
using eternal_api.Application.Distributions.Queries.GetAllDistributionsByPlanogramId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class DistributionEndpoints
    {
        public static IEndpointRouteBuilder MapDistributionEndpoints(this IEndpointRouteBuilder app)
        {
            // Crear una nueva distribución
            app.MapPost("/distributions", async (AddDistributionCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/distributions/{result}", result);
            });

            // Actualizar una distribución existente
            app.MapPut("/distribution/{id:guid}", async (Guid id, UpdateDistributionCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            // Obtener una distribución por Id
            //app.MapGet("/distributions/{id:guid}", async (Guid id, IMediator mediator) =>
            //{
            //    var query = new GetDistributionByIdQuery { Id = id };
            //    var result = await mediator.Send(query);
            //    return result is null
            //        ? Results.NotFound()
            //        : Results.Ok(result);
            //});

            // Listar distribuciones por PlanogramId
            app.MapGet("/distributions/planogram/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetAllDistributionByPlanogramIdQuery { PlanogramId = id };
                var result = await mediator.Send(query);
                return result is null || !result.Any()
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Eliminar una distribución
            app.MapDelete("/distributions/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteDistributionCommand { Id = id };
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}