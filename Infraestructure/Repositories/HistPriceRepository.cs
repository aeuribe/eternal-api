using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class HistPriceRepository : IHistPriceRepository
    {
        private readonly AppDbContext _context;

        public HistPriceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(HistPrice price)
        {
            _context.HistPrices.Add(price);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HistPrice>> GetByProductIdAsync(Guid productId)
        {
            return await _context.HistPrices
                .Where(p => p.ProductId == productId)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<HistPrice?> GetLatestAsync(Guid productId)
        {
            return await _context.HistPrices
                .Where(p => p.ProductId == productId)
                .OrderByDescending(p => p.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task<HistPrice?> GetByDateAsync(Guid productId, DateTime date)
        {
            return await _context.HistPrices
                .Where(p => p.ProductId == productId &&
                            p.StartDate <= date &&
                            (p.EndDate == null || p.EndDate >= date))
                .FirstOrDefaultAsync();
        }
    }
}
