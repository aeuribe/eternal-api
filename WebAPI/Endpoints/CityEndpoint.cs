using eternal_api.Application.Cities.Commands.CreateCity;
using eternal_api.Application.Cities.Commands.UpdateCity;
using eternal_api.Application.Cities.Queries.GetCityByIdQuery;
using eternal_api.Application.Cities.Queries.GetCityByName;
using eternal_api.Application.Cities.Queries.ListCities;

namespace eternal_api.WebAPI.Endpoints
{
    public static class CityEndpoints
    {
        public static void MapCityEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/cities", async (CreateCityCommand command, CreateCityHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/cities/{result}", result);
            });

            app.MapPut("/cities/{id}", async (Guid id, UpdateCityCommand command, UpdateCityHandler handler) =>
            {
                command.Id = id;
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/cities/{id}", async (Guid id, GetCityByIdHandler handler) =>
            {
                var result = await handler.Handle(new GetCityByIdQuery { Id = id });
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/cities/by-name/{name}", async (string name, GetCityByNameHandler handler) =>
            {
                var result = await handler.Handle(new GetCityByNameQuery { Name = name });
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/cities", async (ListCitiesHandler handler) =>
            {
                var result = await handler.Handle(new ListCitiesQuery());
                return Results.Ok(result);
            });
        }
    }


}
