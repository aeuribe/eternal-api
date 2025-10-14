using eternal_api.Application.Stores.Commands.CreateStore;
using eternal_api.Application.Stores.Commands.DesactivateStore;
using eternal_api.Application.Stores.Commands.UpdateStore;
using eternal_api.Application.Stores.Queries.GetStoreById;
using eternal_api.Application.Stores.Queries.GetStoreByName;
using eternal_api.Application.Stores.Queries.GetStoresByCity;
using eternal_api.Application.Stores.Queries.ListStores;

namespace eternal_api.WebAPI.Endpoints
{
    public static class StoreEndpoints
    {
        public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/stores", async (CreateStoreCommand command, CreateStoreHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/stores/{result}", result);
            });

            app.MapPut("/stores/{id}", async (Guid id, UpdateStoreCommand command, UpdateStoreHandler handler) =>
            {
                command.Id = id;
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/stores/deactivate/{id}", async (Guid id, DesactivateStoreHandler handler) =>
            {
                var command = new DesactivateStoreCommand { Id = id };
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/stores/{id}", async (Guid id, GetStoreByIdHandler handler) =>
            {
                var query = new GetStoreByIdQuery { Id = id };
                var result = await handler.Handle(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/stores/by-name/{name}", async (string name, GetStoreByNameHandler handler) =>
            {
                var query = new GetStoreByNameQuery { Name = name };
                var result = await handler.Handle(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/stores/by-city/{cityId}", async (Guid cityId, GetStoresByCityHandler handler) =>
            {
                var query = new GetStoresByCityQuery { CityId = cityId };
                var result = await handler.Handle(query);
                return Results.Ok(result);
            });

            app.MapGet("/stores", async (ListStoresHandler handler) =>
            {
                var result = await handler.Handle(new ListStoresQuery());
                return Results.Ok(result);
            });
        }
    }

}
