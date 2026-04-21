using eternal_api.Domain.Entities;

namespace eternal_api.Application.Presentations.Interfaces
{
    public interface IPresentationRepository
    {
        Task AddAsync(Presentation presentation);
        Task UpdateAsync(Presentation presentation);
        Task<bool> DeleteAsync(Presentation presentation);
        Task<Presentation?> GetByIdAsync(Guid id);
        Task<List<Presentation>> ListAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<Presentation?> GetByGenericCodeAsync(string genericCode);
    }
}
