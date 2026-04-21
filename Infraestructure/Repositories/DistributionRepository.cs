using eternal_api.Application.Distributions.Interfaces;
using eternal_api.Application.Planograms.Commands.UpdatePlanogram;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class DistributionRepository : IDistributionRepository, IValidateOrdersService
    {
        private readonly AppDbContext _context;

        public DistributionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Distribution distribution)
        {
            await _context.Distributions.AddAsync(distribution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Distribution distribution)
        {
            _context.Distributions.Update(distribution);
            await _context.SaveChangesAsync();
        }

        public async Task<Distribution?> GetByIdAsync(Guid id)
        {
            return await _context.Distributions.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Distributions.AnyAsync(d => d.Id == id);
        }

        public async Task<List<Distribution>> ListAsync(Guid planogramId)
        {
            return await _context.Distributions
                .Where(d => d.PlanogramId == planogramId)
                .ToListAsync();
        }

        public async Task DeleteAsync(Distribution distribution)
        {
            _context.Distributions.Remove(distribution);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasOrdersInPlanogramAsync(Guid planogramId)
        {
            return await _context.Orders
                .AnyAsync(order => order.PlanogramId == planogramId);
        }
    }
}