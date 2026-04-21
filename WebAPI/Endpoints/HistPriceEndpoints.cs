using eternal_api.Application.HistPrices.Queries.GetHistPriceByDate;
using eternal_api.Application.HistPrices.Queries.GetLatestHistPrice;
using eternal_api.Application.Prices.Commands.RegisterHistPrice;
using eternal_api.Application.Prices.Queries;
using eternal_api.Application.Prices.Queries.GetHistPriceByDate;
using eternal_api.Application.Prices.Queries.GetHistPriceByProductId;
using eternal_api.Domain.Entities;
using MediatR;

namespace eternal_api.WebAPI.Endpoints
{
    public static class HistPriceEndpoints
    {
        public static void MapHistPriceEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/histprices", async (RegisterHistPriceCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/{result}", result);
            });

            app.MapGet("/histprices/presentation/{presentationId:guid}", async (Guid presentationId, IMediator mediator) =>
            {
                var query = new GetHistPriceByProductIdQuery { PresentationId = presentationId };
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            app.MapGet("/histprices/latest/{presentationId:guid}", async (Guid presentationId, IMediator mediator) =>
            {
                var query = new GetLatestHistPriceQuery { PresentationId = presentationId };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapGet("/histprices/by-date/{presentationId:guid}/{date}", async (Guid presentationId, DateTime date, IMediator mediator) =>
            {
                var query = new GetHistPriceByDateQuery { PresentationId = presentationId, Date = date };
                var result = await mediator.Send(query);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });
        }
    }
}
