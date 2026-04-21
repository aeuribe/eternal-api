

using eternal_api.Application.Assignments.Commands.CreateAssignment;
using eternal_api.Application.Assignments.Commands.DeleteAssignment;
using eternal_api.Application.Assignments.Queries.GetAllVisitLogs;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class AssignmentEndpoints
    {
        public static void MapAssignmentEndpoints(this IEndpointRouteBuilder app)
        {
            // 📌 Registrar asignación
            app.MapPost("/assignments", async ( CreateAssignmentCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            // 📌 Consultar asignaciones
            app.MapGet("/assignments", async ( IMediator mediator) =>
            {
                var query = new GetAllAssignmentsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            // 📌 Eliminar asignación
            app.MapDelete("/assignments/{id}", async ( Guid id, IMediator mediator) =>
            {
                var command = new DeleteAssignmentCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
