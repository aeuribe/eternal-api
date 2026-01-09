using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IVisitLogRepository
    {
        Task<Guid> AddAsync(VisitLog visitLog);
        Task<VisitLog> GetVisitLogByIdAsync(Guid id);
        Task<bool> UpdateAsync(VisitLog visitLog);
        Task<bool> DeleteAsync(Guid id);
        Task<List<VisitLog>> ListAsync();
    }
}

