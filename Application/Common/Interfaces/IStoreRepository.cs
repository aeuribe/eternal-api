using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IStoreRepository
    {
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task<Store?> GetByIdAsync(Guid id);
        Task<Store?> GetByNameAsync(string name);
        Task<List<Store>> GetByCityAsync(Guid cityId);
        Task<List<Store>> ListAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}
