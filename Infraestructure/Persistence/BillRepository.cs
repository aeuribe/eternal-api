using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Infraestructure.Persistence
{
    public class BillRepository : IBillRepository
    {
        public readonly AppDbContext _context;
        public BillRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Bill bill)
        {
            await _context.Bills.AddAsync(bill);

            // Los cambios se vuelven persistentes en la base de datos real únicamente cuando llamas al método SaveChangesAsync()
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bill>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // Dada la posibilidad de retornar null, se agrega un signo de interrogación.
        public async Task<Bill?> GetByIdAsync(Guid id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public Task UpdateAsync(Bill bill)
        {
            throw new NotImplementedException();
        }
    }
}
