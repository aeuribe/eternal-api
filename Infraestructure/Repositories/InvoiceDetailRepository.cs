using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly AppDbContext _context;

        public InvoiceDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InvoiceDetail invoiceDetail)
        {
            await _context.BillDetails.AddAsync(invoiceDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InvoiceDetail invoiceDetail)
        {
            _context.BillDetails.Update(invoiceDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<InvoiceDetail?> GetByIdAsync(Guid id)
        {
            return await _context.BillDetails
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<InvoiceDetail?>> GetAllInvoiceDetailsByInvoiceIdAsync(Guid invoiceId)
        {
            return await _context.BillDetails
                .Where(i => i.InvoiceId == invoiceId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.BillDetails
                .AnyAsync(i => i.Id == id);
        }
    }
}