using eternal_api.Application.Users.Commands.CreateUser;
using eternal_api.Application.Users.Commands.DeactivateUser;
using eternal_api.Application.Users.Commands.UpdateUser;
using eternal_api.Application.Users.Queries.GetUserById;
using eternal_api.Application.Users.Queries.ListUsers;

namespace eternal_api.WebAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (CreateUserCommand command, CreateUserHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/users/{result}", result);
            });

            app.MapPut("/users/{id}", async (Guid id, UpdateUserCommand command, UpdateUserHandler handler) =>
            {
                command.Id = id;
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/users/desactivate/{id}", async (Guid id, DesactivateUserHandler handler) =>
            {
                var command = new DesactivateUserCommand { Id = id };
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/users/{id}", async (Guid id, GetUserByIdHandler handler) =>
            {
                var query = new GetUserByIdQuery { Id = id };
                var result = await handler.Handle(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });


            app.MapGet("/users", async (ListUsersHandler handler) =>
            {
                var result = await handler.Handle(new ListUsersQuery());
                return Results.Ok(result);
            });
        }
    }
}
