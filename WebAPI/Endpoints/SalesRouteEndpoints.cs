using eternal_api.Application.SalesRoutes.Command.CreateSalesRoute;
using eternal_api.Application.SalesRoutes.Command.DeleteSalesRoute;
using eternal_api.Application.SalesRoutes.Command.ToogleStatusSalesRoute;
using eternal_api.Application.SalesRoutes.Command.UpdateSalesRoute;
using eternal_api.Application.SalesRoutes.Queries.GetAllSalesRoutes;
using eternal_api.Application.SalesRoutes.Queries.GetSalesRouteById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace eternal_api.WebAPI.Endpoints
{
    public static class SalesRouteEndpoints
    {
        public static void MapSalesRouteEndpoints(this IEndpointRouteBuilder app)
        {
            // --- GET: / ---
            app.MapGet("/", async (ISender sender) =>
            {
                var routes = await sender.Send(new GetAllSalesRoutesQuery());
                return Results.Ok(routes);
            });

            // --- GET: /{id} ---
            app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var route = await sender.Send(new GetSalesRouteByIdQuery { Id = id });
                return route is not null ? Results.Ok(route) : Results.NotFound();
            });

            // --- POST: / ---
            app.MapPost("/", async (CreateSalesRouteCommand command, ISender sender) =>
            {
                try
                {
                    var routeId = await sender.Send(command);
                    // Como quitamos el grupo aquí, asegúrate de que el frontend sepa la URL completa 
                    // o construye la URI de respuesta adecuadamente si lo necesitas.
                    return Results.Created($"/{routeId}", routeId);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            });

            // --- PUT: /{id} ---
            app.MapPut("/{id:guid}", async (Guid id, UpdateSalesRouteCommand command, ISender sender) =>
            {
                if (id != command.Id)
                    return Results.BadRequest(new { Message = "El ID de la ruta no coincide con el cuerpo de la petición." });

                try
                {
                    var result = await sender.Send(command);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
            });

            // --- PATCH: /{id}/toggle-status ---
            app.MapPatch("/{id:guid}/toggle-status", async (Guid id, ISender sender) =>
            {
                var command = new ToogleStatusSalesRouteCommand { Id = id };
                var result = await sender.Send(command);

                return result ? Results.NoContent() : Results.NotFound();
            });

            // --- DELETE: /{id} ---
            app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    var command = new DeleteSalesRouteCommand { Id = id };
                    var result = await sender.Send(command);

                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    // Regla de negocio: intentó borrar ruta con órdenes. Devolvemos 409 Conflict.
                    return Results.Conflict(new { Message = ex.Message });
                }
            });
        }
    }
}