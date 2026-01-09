using eternal_api.Application.Common.DTOs;
using eternal_api.Application.OrderDetails.Commands.CreateOrderDetail;
using eternal_api.Application.OrderDetails.Commands.UpdateOrderDetail;
using eternal_api.Application.OrderDetails.Queries.GetAllOrderDetailsByOrderId;
using eternal_api.Application.OrderDetails.Queries.GetOrderDetailById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class OrderDetailEndpoints
    {
        public static IEndpointRouteBuilder MapOrderDetailEndpoints(this IEndpointRouteBuilder app)
        {
            // Crear OrderDetail
            app.MapPost("/orderdetails", async (CreateOrderDetailCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/orderdetails/{result}", result);
            });

            // Actualizar OrderDetail
            app.MapPut("/orderdetails/{id:guid}", async (Guid id, UpdateOrderDetailCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            // Obtener todos los OrderDetails de una orden
            app.MapGet("/orderdetails/order/{orderId:guid}", async (Guid orderId, IMediator mediator) =>
            {
                var query = new GetAllOrderDetailsByOrderIdQuery { OrderId = orderId };
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Obtener un OrderDetail por Id
            app.MapGet("/orderdetails/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetOrderDetailByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            return app;
        }
    }
}