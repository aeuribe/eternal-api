using eternal_api.Application.Classes.Interfaces;
using eternal_api.Application.Families.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class FamilyRepository : IFamilyRepository, IClassFamilyValidationService
    {

        private readonly AppDbContext _context;

        public FamilyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsyncFamily(Family family)
        {
            await _context.Families.AddAsync(family);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Family family)
        {
            if (family == null) return false;

            _context.Families.Remove(family);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Families.AnyAsync(b => b.Id == id);
        }

        public async Task<Family?> GetByCodeAsync(string code)
        {
            // Busca la primera familia que coincida con el código e incluye sus relaciones
            return await _context.Families
                .Include(f => f.Brand)
                .Include(f => f.Class)
                .FirstOrDefaultAsync(f => f.FamilyCode.ToLower() == code.ToLower());
        }

        public async Task<Family?> GetByIdAsync(Guid id)
        {
            return await _context.Families
                .Include(f => f.Brand)
                .Include(f => f.Class)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Family?> GetByNameAsync(string name)
        {
            return await _context.Families
                .Include(f => f.Brand)
                .Include(f => f.Class)
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Family>> ListAsync()
        {
            return await _context.Families
                .Include(f => f.Brand)
                .Include(f => f.Class)
                .AsNoTracking() // Optimiza el rendimiento para listas de solo lectura
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task UpdateAsync(Family category)
        {
            _context.Families.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasAnyFamilyAssociatedAsync(Guid classId)
        {
            return await _context.Families
                .AsNoTracking()
                .AnyAsync(f => f.ClassId == classId);
        }
    }
}