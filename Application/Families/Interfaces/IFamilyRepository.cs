using eternal_api.Domain.Entities;

namespace eternal_api.Application.Families.Interfaces
{
    public interface IFamilyRepository
    {
        Task AddAsyncFamily(Family family);
        Task UpdateAsync(Family family);
        Task<Family?> GetByIdAsync(Guid id);
        Task<Family?> GetByCodeAsync(string code);
        Task<Family?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Family>> ListAsync();
        Task<bool> DeleteAsync(Family family);
    }
}
