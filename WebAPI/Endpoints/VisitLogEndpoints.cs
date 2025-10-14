using eternal_api.Application.VisitLogs.Commands.CreateVisitLog;
using eternal_api.Application.VisitLogs.Commands.UpdateVisitLog;
using eternal_api.Application.VisitLogs.Commands.DeleteVisitLog;
using eternal_api.Application.VisitLogs.Queries.ListVisitLogs;
using eternal_api.Application.VisitLogs.Common;

namespace eternal_api.WebAPI.Endpoints
{
    public static class VisitLogEndpoints
    {
        public static void MapVisitLogEndpoints(this IEndpointRouteBuilder app)
        {
            // 📌 Registrar visita
            app.MapPost("/visit-logs", async (
                CreateVisitLogCommand command,
                CreateVisitLogHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/visit-logs/{result}", result);
            });

            // 📌 Consultar historial de visitas
            app.MapGet("/visit-logs", async (
                ListVisitLogsHandler handler) =>
            {
                var result = await handler.Handle(new ListVisitLogsQuery());
                return Results.Ok(result);
            });

            // 📌 Corregir visita
            app.MapPut("/visit-logs/{id}", async (
                Guid id,
                UpdateVisitLogCommand command,
                UpdateVisitLogHandler handler) =>
            {
                command.Id = id;
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // 📌 Anular visita
            app.MapDelete("/visit-logs/{id}", async (
                Guid id,
                DeleteVisitLogHandler handler) =>
            {
                var command = new DeleteVisitLogCommand { Id = id };
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
