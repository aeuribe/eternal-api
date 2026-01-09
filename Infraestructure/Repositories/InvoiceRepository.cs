using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Invoice invoice)
        {
            await _context.Bills.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Bills.
                Include(o => o.Order).
                Include(p => p.POD).
                ToListAsync();
        }

        // Dada la posibilidad de retornar null, se agrega un signo de interrogación.
        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Bills
                .Include(b => b.POD)
                .Include(b => b.Order) 
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task UpdateAsync(Invoice bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }
    }
}
