using eternal_api.Domain.Entities;

namespace eternal_api.Application.Classes.Interfaces
{
    public interface IClassRepository
    {
        Task AddAsync(Class @class);
        Task UpdateAsync(Class @class);
        Task<bool> DeleteAsync(Class @class);
        Task<Class?> GetByIdAsync(Guid id);
        Task<Class?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Class>> ListAsync();
    }
}
