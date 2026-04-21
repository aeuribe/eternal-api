using eternal_api.Application.Products.Commands.CreateProduct;
using eternal_api.Application.Products.Commands.DeactivateProduct; // <-- Agregado para el nuevo comando
using eternal_api.Application.Products.Commands.DeleteProduct;
using eternal_api.Application.Products.Commands.UpdateProduct;
using eternal_api.Application.Products.Queries.GetAllProducts;
using eternal_api.Application.Products.Queries.GetProductById;
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
                return Results.Created($"/{result}", result);
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

            // Eliminar (Borrado Físico con validación de regla de negocio)
            app.MapDelete("/products/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                try
                {
                    var command = new DeleteProductCommand() { Id = id };
                    var result = await mediator.Send(command);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    // Atrapa la excepción de negocio si pertenece a un planograma con órdenes
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            // Desactivar (Borrado Lógico / Soft Delete con validación)
            app.MapPatch("/products/{id:guid}/deactivate", async (Guid id, IMediator mediator) =>
            {
                try
                {
                    var command = new DeactivateProductCommand { Id = id };
                    var result = await mediator.Send(command);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    // Atrapa la excepción de negocio si pertenece a un planograma activo
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            return app;
        }
    }
}