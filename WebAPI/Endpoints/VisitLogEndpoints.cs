using eternal_api.Application.VisitLogs.Commands.CreateVisitLog;
using eternal_api.Application.VisitLogs.Commands.UpdateVisitLog;
using eternal_api.Application.VisitLogs.Commands.DeleteVisitLog;
using eternal_api.Application.VisitLogs.Queries.ListVisitLogs;
using eternal_api.Application.VisitLogs.Common;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class VisitLogEndpoints
    {
        public static void MapVisitLogEndpoints(this IEndpointRouteBuilder app)
        {
            // 📌 Registrar visita
            app.MapPost("/visit-logs", async ( CreateVisitLogCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/visit-logs/{result}", result);
            });

            // 📌 Consultar historial de visitas
            app.MapGet("/visit-logs", async ( IMediator mediator) =>
            {
                var query = new GetAllVisitLogsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            // 📌 Corregir visita
            app.MapPut("/visit-logs/{id}", async ( Guid id, UpdateVisitLogCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // 📌 Anular visita
            app.MapDelete("/visit-logs/{id}", async ( Guid id, IMediator mediator) =>
            {
                var command = new DeleteVisitLogCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
