using eternal_api.Application.SalesRoutes.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class SalesRouteRepository : ISalesRouteRepository
    {
        private readonly AppDbContext _context;

        public SalesRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SalesRoute salesRoute)
        {
            await _context.SalesRoutes.AddAsync(salesRoute);
        }

        public Task DeleteAsync(SalesRoute salesRoute)
        {
            _context.SalesRoutes.Remove(salesRoute);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<SalesRoute>> GetAllAsync()
        {
            return await _context.SalesRoutes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SalesRoute?> GetByCodeAsync(string code)
        {
            return await _context.SalesRoutes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Code == code);
        }

        public async Task<SalesRoute?> GetByIdAsync(Guid id)
        {
            // La entidad se recupera RASTREADA (Tracked)
            return await _context.SalesRoutes
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> CountByCityAsync(Guid cityId)
        {
            return await _context.SalesRoutes.CountAsync(r => r.CityId == cityId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(SalesRoute salesRoute)
        {
            // FIX: Verificamos el estado de la entidad antes de intentar actualizarla.
            // Si la entidad ya está siendo rastreada (porque se usó GetByIdAsync), 
            // no hacemos nada, EF Core ya sabe que cambió.
            var entry = _context.Entry(salesRoute);

            if (entry.State == EntityState.Detached)
            {
                _context.SalesRoutes.Update(salesRoute);
            }

            return Task.CompletedTask;
        }

        public Task UpdateStatusAsync(SalesRoute salesRoute)
        {
            // FIX: Misma protección aquí. Si la entidad ya está rastreada, 
            // hacer Attach lanzaría el mismo error.
            var entry = _context.Entry(salesRoute);

            if (entry.State == EntityState.Detached)
            {
                _context.SalesRoutes.Attach(salesRoute);
            }

            // Solo marcamos la propiedad IsActive como modificada
            entry.Property(r => r.IsActive).IsModified = true;

            return Task.CompletedTask;
        }

        public async Task<string?> GetLastCodeByStatePrefixAsync(string statePrefix)
        {
            // Buscamos algo como "FL-"
            var prefixSearch = $"{statePrefix}-";

            var lastRoute = await _context.SalesRoutes
                .Where(r => r.Code.StartsWith(prefixSearch))
                .OrderByDescending(r => r.Code)
                .FirstOrDefaultAsync();

            return lastRoute?.Code;
        }
    }
}