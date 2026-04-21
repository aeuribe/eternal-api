using eternal_api.Application.Cities.Commands.CreateCity;
using eternal_api.Application.Cities.Commands.UpdateCity;
using eternal_api.Application.Cities.Queries.GetCityByIdQuery;
using eternal_api.Application.Cities.Queries.GetCityByName;
using eternal_api.Application.Cities.Queries.ListCities;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class CityEndpoints
    {
        public static void MapCityEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/cities", async (CreateCityCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("/cities/{id:guid}", async (Guid id, UpdateCityCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/cities/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetCityByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/cities/by-name/{name}", async (string name, IMediator mediator) =>
            {
                var query = new GetCityByNameQuery { Name = name };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/cities", async (IMediator mediator) =>
            {
                var query = new GetAllCitiesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });
        }
    }


}
