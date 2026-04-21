using eternal_api.Application.Users.Commands.AssignUserToRoute; // <--- El nuevo using
using eternal_api.Application.Users.Commands.DeactivateUser; // (Nota: Sugiero renombrar tu carpeta/clase a Deactivate)
using eternal_api.Application.Users.Commands.UpdateUser;
using eternal_api.Application.Users.Queries.GetUserById;
using eternal_api.Application.Users.Queries.ListUsers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace eternal_api.WebAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            // --- PUT: /users/{id} ---
            app.MapPut("/users/{id:guid}", async (Guid id, UpdateUserCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // --- PUT: /users/desactivate/{id} ---
            // 💡 Tip de Arquitecto: Considera usar PATCH para cambios de estado, ej: app.MapPatch("/users/{id:guid}/toggle-status"
            app.MapPut("/users/desactivate/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DesactivateUserCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // ==========================================
            // NUEVO ENDPOINT: Asignar o quitar territorio
            // ==========================================
            // --- PUT: /users/{id}/assign-route ---
            app.MapPut("/users/{id:guid}/assign-route", async (Guid id, AssignUserToRouteCommand command, IMediator mediator) =>
            {
                // Aseguramos que el ID de la URL sea el que se procesa
                command.UserId = id;

                try
                {
                    var success = await mediator.Send(command);
                    return success ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    // Si la ruta no existe o hay un error de negocio, le devolvemos un 400 Bad Request a Next.js
                    return Results.BadRequest(new { Message = ex.Message });
                }
            });

            // --- GET: /users/{id} ---
            app.MapGet("/users/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetUserByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            // --- GET: /users ---
            app.MapGet("/users", async (IMediator mediator) =>
            {
                var query = new GetAllUsersQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}