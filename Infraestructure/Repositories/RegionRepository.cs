using eternal_api.Application.Regions.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _context;

        public RegionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions
                .Include(r => r.Area) // Traemos el Área asociada
                .AsNoTracking()       // Optimizamos la lectura en memoria
                .OrderBy(r => r.Name) // Ordenamos alfabéticamente
                .ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions
                .Include(r => r.Area) // Traemos el Área asociada
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Region region)
        {
            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Region region)
        {
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasDistrictsAsync(Guid regionId)
        {
            // Verificamos si existe al menos un Distrito atado a esta Región
            return await _context.Districts
                .AsNoTracking()
                .AnyAsync(d => d.RegionId == regionId);
        }
    }
}
