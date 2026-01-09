using eternal_api.Application.Common.Interfaces;
using eternal_api.Application.Prices.Commands.RegisterHistPrice;
using eternal_api.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eternal_api.Application.Prices.Commands.UpdateHistPrice
{
    public class UpdateHistPriceHandler : IRequestHandler<UpdateHistPriceCommand, Guid>
    {
        private readonly IHistPriceRepository _histRepository;
        public UpdateHistPriceHandler(IHistPriceRepository histRepository) 
        {
            _histRepository = histRepository;
        }

        public async Task<Guid> Handle(UpdateHistPriceCommand command, CancellationToken cancellationToken)
        {
            var price = await _histRepository.GetLatestAsync(command.ProductId);

            if (price == null) return Guid.Empty;

            
            price.EndDate = DateTime.UtcNow;

            var newPrice = new HistPrice(command.ProductId, command.Price, DateTime.UtcNow );

            await _histRepository.AddAsync(newPrice);

            // Retornar el Id del registro actualizado
            return newPrice.Id;
        }
    }
}
