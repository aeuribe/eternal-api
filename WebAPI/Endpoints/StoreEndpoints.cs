using eternal_api.Application.Stores.Commands.CreateStore;
using eternal_api.Application.Stores.Commands.DesactivateStore;
using eternal_api.Application.Stores.Commands.UpdateStore;
using eternal_api.Application.Stores.Queries.GetStoreById;
using eternal_api.Application.Stores.Queries.GetStoresByCity;
using eternal_api.Application.Stores.Queries.GetStoresByDistrict; // <-- El nuevo namespace
using eternal_api.Application.Stores.Queries.ListStores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class StoreEndpoints
    {
        public static void MapStoreEndpoints(this IEndpointRouteBuilder routes)
        {
            // Agrupamos bajo /api/stores y le damos una etiqueta para Swagger
            var group = routes.MapGroup("/stores").WithTags("Stores");

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateStoreCommand command) =>
            {
                var id = await mediator.Send(command);
                // Retornamos el ID de la tienda recién creada
                return Results.Created($"/api/stores/{id}", new { Id = id });
            });

            group.MapPut("/{id:guid}", async (Guid id, IMediator mediator, [FromBody] UpdateStoreCommand command) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound("Tienda no encontrada.");
            });

            group.MapPut("/deactivate/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DesactivateStoreCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound("Tienda no encontrada.");
            });

            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetStoreByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound("Tienda no encontrada.") : Results.Ok(result);
            });

            group.MapGet("/by-city/{cityId:guid}", async (Guid cityId, IMediator mediator) =>
            {
                var query = new GetStoresByCityQuery { CityId = cityId };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            // NUEVO: Endpoint para buscar por Distrito
            group.MapGet("/by-district/{districtId:guid}", async (Guid districtId, IMediator mediator) =>
            {
                // Aquí usamos la sintaxis del record que pasamos por constructor
                var query = new GetStoresByDistrictQuery(districtId);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            group.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetAllStoresQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }
}