using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Products.Commands.CreateProduct;
using eternal_api.Application.Products.Commands.DeleteProduct;
using eternal_api.Application.Products.Commands.UpdateProduct;
using eternal_api.Application.Products.Queries.GetAllProducts;
using eternal_api.Application.Products.Queries.GetProductById;
using eternal_api.Application.Products.Queries.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            // Crear Producto
            app.MapPost("/products", async (CreateProductCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/products/{result}", result);
            });

            // Obtener Producto por Id
            app.MapGet("/products/{id:guid}", async (Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetProductByIdQuery { Id = id };
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Obtener todos los productos
            app.MapGet("/products", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllProductsQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            // Actualizar
            app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductCommand command, IMediator mediator) =>
            {
                // Aseguras que el id de la ruta prevalezca sobre el body
                command.Id = id;

                var result = await mediator.Send(command);

                return result
                    ? Results.NoContent()   // 204 si se actualizó correctamente
                    : Results.NotFound();   // 404 si no existe el recurso
            });

            // Eliminar
            app.MapDelete("/products/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteProductCommand() {Id = id } ;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            // Por categoría
            app.MapGet("products/category/{category}", async ([FromQuery] string category, IMediator mediator) =>
            {
                var query = new GetProductsByCategoryQuery() {Category = category };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return app;
        }
    }
}
