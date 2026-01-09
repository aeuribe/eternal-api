using eternal_api.Application.Users.Commands.CreateUser;
using eternal_api.Application.Users.Commands.DeactivateUser;
using eternal_api.Application.Users.Commands.UpdateUser;
using eternal_api.Application.Users.Queries.GetUserById;
using eternal_api.Application.Users.Queries.ListUsers;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (CreateUserCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/users/{result}", result);
            });

            app.MapPut("/users/{id:guid}", async (Guid id, UpdateUserCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/users/desactivate/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DesactivateUserCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/users/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetUserByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });


            app.MapGet("/users", async (IMediator mediator) =>
            {
                var query = new GetAllUsersQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
