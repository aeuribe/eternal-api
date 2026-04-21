using eternal_api.Application.Bills.Commands.CreateBill;
using eternal_api.Application.Bills.Queries.GetAllBills;
using eternal_api.Application.Bills.Queries.GetBillById;
using eternal_api.Application.Invoices.Commands.AssignPOD;
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
                return Results.Created($"/{result}", result);
            });

            //// Obtener Invoice por Id
            app.MapGet("/invoices/{id:guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
            {
                var query = new GetInvoiceByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);

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

            app.MapPatch("/invoices/{id:guid}/pod", async ([FromRoute] Guid id, [FromBody] string podUrl, [FromServices] IMediator mediator) =>
            {
                // Instanciamos el comando mapeando el ID de la URL y el string del Body
                var command = new AssignPODCommand
                {
                    Id = id,
                    POD = podUrl
                };

                var result = await mediator.Send(command);

                // Si MediatR devuelve true, retornamos un 204 No Content (Estándar para actualizaciones exitosas sin cuerpo de respuesta)
                return result ? Results.NoContent() : Results.BadRequest("No se pudo actualizar el POD.");
            });

            return app;
        }
    }
}
