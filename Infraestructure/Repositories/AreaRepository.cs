using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AppDbContext _context;

        public AreaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Area area)
        {
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _context.Areas
                .AsNoTracking() // Fundamental para optimizar lecturas
                .OrderBy(a => a.Name) // Ordenamos alfabéticamente para que el frontend lo reciba organizado
                .ToListAsync();
        }

        public async Task<Area?> GetByIdAsync(Guid id)
        {
            return await _context.Areas.FindAsync(id);
        }

        public async Task UpdateAsync(Area area)
        {
            _context.Areas.Update(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Area area)
        {
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasRegionsAsync(Guid areaId)
        {
            // Verificamos si existe al menos una región atada a esta área
            // AsNoTracking lo hace más ligero y AnyAsync evita cargar toda la tabla
            return await _context.Regions
                .AsNoTracking()
                .AnyAsync(r => r.AreaId == areaId);
        }
    }
}