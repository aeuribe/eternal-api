using eternal_api.Application.Brands.Commands.CreateBrand;
using eternal_api.Application.Brands.Commands.DesactivateBrand;
using eternal_api.Application.Brands.Commands.UpdateBrand;
using eternal_api.Application.Brands.Queries.GetAllBrands;
using eternal_api.Application.Brands.Queries.GetBrandById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class BrandEndpoint
    {
        public static void MapBrandEndpoints( this IEndpointRouteBuilder app) 
        { 
            app.MapPost("/brands", async (CreateBrandCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("brands/{id:guid}", async (Guid id, UpdateBrandCommand command, IMediator mediator) => 
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("brands/desactivate/{id:guid}", async (Guid id, DesactivateBrandCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("brands", async (IMediator mediator) => 
            {
                var query = new GetAllBrandsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("brands/{id:Guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetBrandByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

        }
    }
}
