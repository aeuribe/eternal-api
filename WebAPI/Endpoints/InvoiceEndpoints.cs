using eternal_api.Application.Bills.Commands.AssignPod;
using eternal_api.Application.Bills.Commands.CreateBill;
using eternal_api.Application.Bills.Commands.UpdateBill;
using eternal_api.Application.Bills.Queries.GetAllBills;
using eternal_api.Application.Bills.Queries.GetBillById;
using eternal_api.Application.invoices.Commands.Createinvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace eternal_api.WebAPI.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static IEndpointRouteBuilder MapInvoiceEndpoints (this IEndpointRouteBuilder app)
        {
            // Crear Invoice
            app.MapPost("/invoices", async (CreateInvoiceCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/invoices/{result}", result);
            });

            // Update Invoice
            app.MapPut("/invoices/{id:guid}", async(Guid id, UpdateInvoiceCommand command, [FromServices] IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            });

            // Create and Assing POD
            app.MapPost("/invoices/{id:guid}/pod", async (Guid id, AssignPodCommand command, [FromServices] IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Created($"/invoice/{result}", result);
            });

            //// Obtener Invoice por Id
            app.MapGet("/invoices/{id:guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetInvoiceByIdQuery();
                query.Id = id;
                var result = mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            ////Obtener todos los Invoices
            app.MapGet("/invoices", async ([FromServices] IMediator mediator) =>
            {
                var query = new GetAllInvoicesQuery();
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            return app;
        }
    }
}
