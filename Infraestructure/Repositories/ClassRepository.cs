using eternal_api.Application.Classes.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Class @class)
        {
            await _context.Classes.AddAsync(@class);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class @class)
        {
            _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Class @class)
        {
            if (@class is null) return false;
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Class?> GetByNameAsync(string name)
        {
            return await _context.Classes.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Classes.AnyAsync(c => c.Id == id);
        }

        public async Task<List<Class>> ListAsync()
        {
            return await _context.Classes.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
