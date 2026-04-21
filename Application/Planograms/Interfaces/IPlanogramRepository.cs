using eternal_api.Domain.Entities;

namespace eternal_api.Application.Planograms.Interfaces
{
    public interface IPlanogramRepository
    {
        Task AddAsync(Planogram planogram);
        Task UpdateAsync(Planogram planogram);
        Task<Planogram?> GetPlanogramById(Guid id);
        Task<IEnumerable<Planogram>> GetAllAsync();
        Task DeleteAsync(Planogram planogram);
    }
}
