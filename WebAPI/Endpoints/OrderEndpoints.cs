using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Orders.Commands.CreateOrder;
using eternal_api.Application.Orders.Commands.UpdateOrder;
using eternal_api.Application.Orders.Commands.UpdateStatus;
using eternal_api.Application.Orders.Queries.GetAllOrders;
using eternal_api.Application.Orders.Queries.GetOrderById;
using eternal_api.Application.Orders.Queries.GetOrderBySalespersonId;
using eternal_api.Application.Orders.Queries.GetOrdersByStoreId;
using eternal_api.Application.Orders.Commands.DeleteOrder;
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
                return Results.Created($"/orders/{result}", result);
            });

            app.MapPut("/order/{id:guid}", async (Guid id, UpdateOrderCommand command, IMediator mediator) =>
            {
                command.OrderId = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
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
                var result = mediator.Send(query);
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
