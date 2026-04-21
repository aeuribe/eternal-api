using eternal_api.Domain.Entities;

namespace eternal_api.Application.SalesRoutes.Interfaces
{
    public interface ISalesRouteRepository
    {
        // --- Consultas ---
        Task<SalesRoute?> GetByIdAsync(Guid id);
        Task<IEnumerable<SalesRoute>> GetAllAsync();
        Task<SalesRoute?> GetByCodeAsync(string code);
        Task<int> CountByCityAsync(Guid cityId);
        Task<string?> GetLastCodeByStatePrefixAsync(string statePrefix);

        // --- Comandos ---
        Task AddAsync(SalesRoute salesRoute);
        Task UpdateAsync(SalesRoute salesRoute);
        Task UpdateStatusAsync(SalesRoute salesRoute);
        Task DeleteAsync(SalesRoute salesRoute);

        // --- Persistencia ---
        Task<int> SaveChangesAsync();
    }
}