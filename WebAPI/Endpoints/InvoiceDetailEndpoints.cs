using eternal_api.Application.InvoiceDetails.Commands.CreateInvoiceDetail;
using eternal_api.Application.InvoiceDetails.Commands.UpdateInvoiceDetail;
using eternal_api.Application.InvoiceDetails.Queries.GetAllInvoiceDetailsByInvoiceId;
using eternal_api.Application.InvoiceDetails.Queries.GetInvoiceDetailById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eternal_api.WebAPI.Endpoints
{
    public static class InvoiceDetailEndpoints
    {
        public static IEndpointRouteBuilder MapInvoiceDetailEndpoints(this IEndpointRouteBuilder app)
        {
            // Crear InvoiceDetail
            app.MapPost("/invoicedetails", async (CreateInvoiceDetailCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/invoicedetails/{result}", result);
            });

            // Actualizar InvoiceDetail
            app.MapPut("/invoicedetails/{id:guid}", async (Guid id, UpdateInvoiceDetailCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            // Obtener todos los InvoiceDetails de una Invoice
            app.MapGet("/invoicedetails/invoice/{invoiceId:guid}", async (Guid invoiceId, IMediator mediator) =>
            {
                var query = new GetAllInvoiceDetailsByInvoiceIdQuery { InvoiceId = invoiceId };
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            // Obtener un InvoiceDetail por Id
            app.MapGet("/invoicedetails/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetInvoiceDetailByIdQuery { Id = id };
                var result = await mediator.Send(query);
                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            return app;
        }
    }
}