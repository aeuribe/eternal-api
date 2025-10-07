using eternal_api.Application.HistPrices.Queries.GetHistPriceByDate;
using eternal_api.Application.HistPrices.Queries.GetLatestHistPrice;
using eternal_api.Application.Prices.Commands.RegisterHistPrice;
using eternal_api.Application.Prices.Queries;
using eternal_api.Application.Prices.Queries.GetHistPriceByDate;
using eternal_api.Application.Prices.Queries.GetHistPriceByProductId;

namespace eternal_api.WebAPI.Endpoints
{
    public static class HistPriceEndpoints
    {
        public static void MapHistPriceEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/histprices", async (RegisterHistPriceCommand command, RegisterHistPriceHandler handler) =>
            {
                var result = await handler.Handle(command);
                return Results.Created($"/histprices/{result}", result);
            });

            app.MapGet("/histprices/product/{productId:guid}", async (Guid productId, GetHistPriceByProductIdHandler handler) =>
            {
                var query = new GetHistPriceByProductIdQuery { ProductId = productId };
                var result = await handler.Handle(query);
                return Results.Ok(result);
            });

            app.MapGet("/histprices/latest/{productId:guid}", async (Guid productId, GetLatestHistPriceHandler handler) =>
            {
                var query = new GetLatestHistPriceQuery { ProductId = productId };
                var result = await handler.Handle(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/histprices/by-date/{productId:guid}/{date}", async (Guid productId, DateTime date, GetHistPriceByDateHandler handler) =>
            {
                var query = new GetHistPriceByDateQuery { ProductId = productId, Date = date };
                var result = await handler.Handle(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });
        }
    }
}
