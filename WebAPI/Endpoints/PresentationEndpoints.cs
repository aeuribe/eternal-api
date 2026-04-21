using eternal_api.Application.Presentations.Commands.CreatePresentation;
using eternal_api.Application.Presentations.Commands.DeletePresentation;
using eternal_api.Application.Presentations.Commands.ToggleStatus;
using eternal_api.Application.Presentations.Commands.UpdatePresentation;
using eternal_api.Application.Presentations.Queries.GetAllPresentations;
using eternal_api.Application.Presentations.Queries.GetPresentationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class PresentationEndpoints
    {
        public static IEndpointRouteBuilder MapPresentationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/presentations", async (CreatePresentationCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("/presentations/{id:guid}", async (Guid id, UpdatePresentationCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/presentations", async (IMediator mediator) =>
            {
                var query = new GetAllPresentationsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("/presentations/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetPresentationByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapDelete("/presentations/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var command = new DeletePresentationCommand { Id = id };
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapPatch("/presentations/{id:guid}/toggle-status", async (Guid id, IMediator mediator) =>
            {
                var command = new ToggleStatusPresentationCommand(id);
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
