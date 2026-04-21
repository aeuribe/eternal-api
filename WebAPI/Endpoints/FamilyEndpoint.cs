using eternal_api.Application.Brands.Commands.CreateBrand;
using eternal_api.Application.Brands.Commands.DesactivateBrand;
using eternal_api.Application.Brands.Commands.UpdateBrand;
using eternal_api.Application.Brands.Queries.GetAllBrands;
using eternal_api.Application.Brands.Queries.GetBrandById;
using eternal_api.Application.Families.Commands.CreateFamily;
using eternal_api.Application.Families.Commands.DeleteFamily;
using eternal_api.Application.Families.Commands.DesactivateCategory;
using eternal_api.Application.Families.Commands.UpdateCategory;
using eternal_api.Application.Families.Queries.GetAllCategories;
using eternal_api.Application.Families.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class FamilyEndpoint
    {
        public static void MapFamilyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/families", async (CreateFamilyCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("families/{id:guid}", async (Guid id, UpdateFamilyCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapPut("families/desactivate/{id:guid}", async (Guid id, DesactivateActivateFamilyCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("families", async (IMediator mediator) =>
            {
                var query = new GetAllFamiliesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("families/{id:Guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetFamilyByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapDelete("families/{id:Guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var command = new DeleteFamilyCommand { Id = id };
                var result = await mediator.Send(command);

                return result ? Results.NoContent() : Results.NotFound();
            });

        }
    }
}
