using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IDistributionRepository
    {
        Task AddAsync(Distribution distribution);
        Task UpdateAsync(Distribution distribution);
        Task<Distribution?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Distribution>> ListAsync(Guid planogramId);
        Task DeleteAsync(Distribution distribution);
    }
}
