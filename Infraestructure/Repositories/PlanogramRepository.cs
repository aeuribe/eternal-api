using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class PlanogramRepository : IPlanogramRepository
    {
        public readonly AppDbContext _context;

        public PlanogramRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Planogram planogram)
        {
            await _context.Planograms.AddAsync(planogram);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Planogram planogram)
        {
            _context.Planograms.Remove(planogram);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Planogram>> GetAllAsync()
        {
            return await _context.Planograms.
                ToListAsync();
        }

        public async Task<Planogram?> GetPlanogramById(Guid id)
        {
            return await _context.Planograms.FindAsync(id);
        }

        public async Task UpdateAsync(Planogram planogram)
        {
            _context.Planograms.Update(planogram);
            await _context.SaveChangesAsync();
        }
    }
}
