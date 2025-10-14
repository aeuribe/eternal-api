using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class VisitLogRepository : IVisitLogRepository
    {
        private readonly AppDbContext _context;

        public VisitLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(VisitLog visitLog)
        {
            _context.VisitLogs.Add(visitLog);
            await _context.SaveChangesAsync();
            return visitLog.Id;
        }

        public async Task<bool> UpdateAsync(VisitLog visitLog)
        {
            _context.VisitLogs.Update(visitLog);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.VisitLogs.FindAsync(id);
            if (entity is null) return false;
            _context.VisitLogs.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<VisitLog>> ListAsync()
        {
            return await _context.VisitLogs
                .Include(v => v.Store)
                .Include(v => v.Salesperson)
                .ToListAsync();
        }
    }
}

