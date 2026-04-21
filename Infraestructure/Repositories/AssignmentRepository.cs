using eternal_api.Application.Assignments.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment.Id;
        }

        public async Task<bool> UpdateAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Assignments.FindAsync(id);
            if (entity is null) return false;
            _context.Assignments.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Assignment>> ListAsync()
        {
            return await _context.Assignments
                .ToListAsync();
        }
        public async Task<Assignment> GetAssignmentByIdAsync(Guid id)
        {
            return await _context.Set<Assignment>()
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Assignment?> GetByStoreIdAsync(Guid storeId)
        {
            // Retornamos el primer registro que coincida con el ID de la tienda.
            // (Recuerda que en tu Configuration pusimos que StoreId es Unique, 
            // así que garantizamos que máximo habrá un registro activo).
            return await _context.Assignments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.StoreId == storeId);
        }
    }
}

