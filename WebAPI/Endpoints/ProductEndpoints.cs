using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Products.Commands.CreateProduct;
using eternal_api.Application.Products.Commands.DeleteProduct;
using eternal_api.Application.Products.Commands.UpdateProduct;
using eternal_api.Application.Products.Queries.GetAllProducts;
using eternal_api.Application.Products.Queries.GetProductById;
using eternal_api.Application.Products.Queries.GetProductsByCategory;

namespace eternal_api.WebAPI.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            // Crear Producto
            app.MapPost("/products", async (CreateProductCommand command, CreateProductHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/products/{result}", result);
            });

            // Obtener Producto por Id
            app.MapGet("/products/{id:guid}", async (Guid id, GetProductByIdHandler handler) =>
            {
                var query = new GetProductByIdQuery { Id = id };
                var result = await handler.Handle(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Obtener todos los productos
            app.MapGet("/products", async (GetAllProductsHandler handler) =>
            {
                var result = await handler.Handle(new GetAllProductsQuery());
                return Results.Ok(result);
            });

            // Actualizar
            app.MapPut("/{id:guid}", async (IProductRepository repo, Guid id, UpdateProductCommand command) =>
            {
                if (id != command.Id) return Results.BadRequest("Id mismatch");
                var handler = new UpdateProductHandler(repo);
                await handler.Handle(command);
                return Results.NoContent();
            });

            // Eliminar
            app.MapDelete("/products/{id:guid}", async (Guid id, DeleteProductHandler handler) =>
            {
                var command = new DeleteProductCommand { Id = id };
                var success = await handler.Handle(command);
                return success ? Results.NoContent() : Results.NotFound();
            });

            // Por categoría
            app.MapGet("/category/{category}", async (IProductRepository repo, string category) =>
            {
                var handler = new GetProductsByCategoryHandler(repo);
                var list = await handler.Handle(new GetProductsByCategoryQuery { Category = category });
                return Results.Ok(list);
            });

            return app;
        }
    }
}
