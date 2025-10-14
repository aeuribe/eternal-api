using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IHistPriceRepository
    {
        Task AddAsync(HistPrice price);
        Task<List<HistPrice>> GetByProductIdAsync(Guid productId);
        Task<HistPrice?> GetLatestAsync(Guid productId);
        Task<HistPrice?> GetByDateAsync(Guid productId, DateTime date);
    }
}
