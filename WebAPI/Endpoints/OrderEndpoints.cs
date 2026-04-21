
using eternal_api.Application.Orders.Commands.CreateOrder;
using eternal_api.Application.Orders.Commands.DeleteOrder;
using eternal_api.Application.Orders.Commands.UpdateOrder;
using eternal_api.Application.Orders.Commands.UpdateStatus;
using eternal_api.Application.Orders.Queries.GetAllOrders;
using eternal_api.Application.Orders.Queries.GetOrderById;
using eternal_api.Application.Orders.Queries.GetOrderBySalespersonId;
using eternal_api.Application.Orders.Queries.GetOrderDiscrepancies;
using eternal_api.Application.Orders.Queries.GetOrdersByStoreId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class OrderEndpoints
    {
        public static IEndpointRouteBuilder MapOrderEndpoints (this IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderCommand command, IMediator mediator) => 
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapPut("/orders/{id:guid}", async (Guid id, [FromBody] UpdateOrderCommand command, IMediator mediator) =>
            {
                // Aseguramos que el ID de la ruta sea el que se procesa en el comando
                command.Id = id;

                try
                {
                    var result = await mediator.Send(command);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    // Captura la excepción de estado (ej: "Solo se pueden modificar pedidos en estado Creado")
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    // Captura cualquier otro error (ej: "La orden no existe")
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            app.MapPut("/order/{id}/status", async (Guid id, UpdateStatusCommand command, IMediator mediator) =>
            {
                command.OrderId = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            app.MapGet("/orders", async ([FromServices] IMediator mediator) => 
            {
                var query = new GetAllOrdersQuery();
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            app.MapGet("/orders/{id:guid}", async (Guid id, IMediator mediator) => 
            {
                var query = new GetOrderByIdQuery();
                query.OrderId = id;
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            app.MapGet("/orders/user/{id:guid}", async (Guid id, [FromServices] IMediator mediator) => 
            {
                var query = new GetOrdersBySalespersonIdQuery();
                query.SalespersonId = id;
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            app.MapGet("/orders/store/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetOrdersByStoreIdQuery();
                query.StoreId = id;
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            app.MapGet("/orders/dicrepancies/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetOrderDiscrepanciesQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            app.MapDelete("/orders/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteOrderCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
