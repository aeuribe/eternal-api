using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IBillRepository
    {
        Task AddAsync(Bill bill);
        Task UpdateAsync(Bill bill);
        Task<Bill?> GetByIdAsync(Guid id);
        Task<IEnumerable<Bill>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
