using eternal_api.Application.Classes.Commands.CreateClass;
using eternal_api.Application.Classes.Commands.DeleteClass;
using eternal_api.Application.Classes.Commands.ToggleStatusClass;
using eternal_api.Application.Classes.Commands.UpdateClass;
using eternal_api.Application.Classes.Queries.GetAllClasses;
using eternal_api.Application.Classes.Queries.GetClassById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class ClassEndpoints
    {
        public static IEndpointRouteBuilder MapClassEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/classes", async (CreateClassCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("/classes/{id:guid}", async (Guid id, UpdateClassCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/classes/desactivate/{id:guid}", async (Guid id, ToggleStatusClassCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/classes", async (IMediator mediator) =>
            {
                var query = new GetAllClassesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("/classes/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetClassByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapDelete("/classes/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                try
                {
                    var command = new DeleteClassCommand { Id = id };
                    var result = await mediator.Send(command);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { Message = ex.Message });
                }
            });

            return app;
        }
    }
}
