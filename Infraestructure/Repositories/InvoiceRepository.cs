using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository, IInvoiceProvider
    {
        public readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Invoice invoice)
        {
            await _context.Bills.AddAsync(invoice);
        }

        public async Task AddPodAsync(Invoice invoice)
        {
            // Le decimos a EF que solo el campo Pod ha sido modificado
            _context.Entry(invoice).Property(x => x.POD).IsModified = true;

        }
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Bills.
                AsNoTracking().
                Include(o => o.Order).
                Include(id => id.invoiceDetails).
                ToListAsync();
        }
        // Dada la posibilidad de retornar null, se agrega un signo de interrogación.
        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Bills
                .Include(b => b.Order) 
                .Include(id => id.invoiceDetails)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Invoice?> GetInvoiceByOrderIdAsync(Guid orderId)
        {
            return await _context.Bills
                .AsNoTracking() // Ideal para lectura rápida sin bloquear memoria
                                // Asumo que tu propiedad de navegación hacia los detalles se llama BillDetails o InvoiceDetails. 
                                // Cambia 'BillDetails' por el nombre real que tengas en tu entidad Invoice.
                .Include(i => i.invoiceDetails)
                .FirstOrDefaultAsync(i => i.OrderId == orderId);
        }

        public async Task<string?> GetLastInvoiceNumberByRouteCodeAsync(string routeCode)
        {
            // Buscamos facturas que en su número contengan el prefijo de la ruta (Ej: "-FL01-")
            return await _context.Bills
                .AsNoTracking()
                .Where(i => i.InvoiceNumber.Contains($"-{routeCode}-"))
                .OrderByDescending(i => i.InvoiceNumber) // Z a A (El número más alto queda de primero)
                .Select(i => i.InvoiceNumber) // Extraemos SOLO el string, no toda la factura
                .FirstOrDefaultAsync(); // Devuelve null si es la primera factura de esa ruta
        }


    }
}
