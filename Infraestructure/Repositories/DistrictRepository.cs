using eternal_api.Application.Districts.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext _context;

        public DistrictRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(District district)
        {
            await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(District district)
        {
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(District district)
        {
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<District>> GetAllAsync()
        {
            return await _context.Districts
                .Include(d => d.Region)
                    .ThenInclude(r => r.Area) // Traemos la jerarquía completa
                .AsNoTracking() // Optimiza la memoria al no rastrear cambios en esta lista
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<District?> GetByIdAsync(Guid id)
        {
            return await _context.Districts
                .Include(d => d.Region)
                    .ThenInclude(r => r.Area) // Traemos la jerarquía completa
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> HasStoresAsync(Guid districtId)
        {
            // Verificamos de forma muy rápida si existe al menos una tienda en este distrito
            return await _context.Stores
                .AsNoTracking()
                .AnyAsync(s => s.DistrictId == districtId);
        }
    }
}
