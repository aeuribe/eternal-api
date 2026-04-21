using eternal_api.Domain.Entities;

namespace eternal_api.Application.Prices.Interfaces
{
    public interface IHistPriceRepository
    {
        Task AddAsync(HistPrice price);
        Task<List<HistPrice>> GetByPresentationIdAsync(Guid presentationId);
        Task<HistPrice?> GetLatestAsync(Guid presentationId);
        Task<HistPrice?> GetByDateAsync(Guid presentationId, DateTime date);
    }
}
