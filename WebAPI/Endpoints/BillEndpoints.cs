using eternal_api.Application.Bills.Commands.CreateBill;
using eternal_api.Application.Bills.Queries.GetBillById;

namespace eternal_api.WebAPI.Endpoints
{
    public static class BillEndpoints
    {
        public static IEndpointRouteBuilder MapBillEndpoints (this IEndpointRouteBuilder app)
        {
            // Crear Bill
            app.MapPost("/bills", async (CreateBillCommand command, CreateBillHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/bills/{result}", result);
            });

            // Obtener Bill por Id
            app.MapGet("/bills/{id:guid}", async (Guid id, GetBillByIdHandler handler) =>
            {
                var query = new GetBillByIdQuery { Id = id };
                var result = handler.Handle(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

            return app;
        }
    }
}
