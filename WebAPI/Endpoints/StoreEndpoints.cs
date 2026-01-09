using eternal_api.Application.Stores.Commands.CreateStore;
using eternal_api.Application.Stores.Commands.DesactivateStore;
using eternal_api.Application.Stores.Commands.UpdateStore;
using eternal_api.Application.Stores.Queries.GetStoreById;
using eternal_api.Application.Stores.Queries.GetStoreByName;
using eternal_api.Application.Stores.Queries.GetStoresByCity;
using eternal_api.Application.Stores.Queries.ListStores;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class StoreEndpoints
    {
        public static void MapStoreEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/stores", async (CreateStoreCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/stores/{result}", result);
            });

            app.MapPut("/stores/{id:guid}", async (Guid id, UpdateStoreCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("/stores/deactivate/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DesactivateStoreCommand { Id = id };
                var success = await mediator.Send(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/stores/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetStoreByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/stores/by-name/{name}", async (string name, IMediator mediator) =>
            {
                var query = new GetStoreByNameQuery { Name = name };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/stores/by-city/{cityId}", async (Guid cityId, IMediator mediator) =>
            {
                var query = new GetStoresByCityQuery { CityId = cityId };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("/stores", async (IMediator mediator) =>
            {
                var query = new GetAllStoresQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }

}
