using eternal_api.Application.Invoices.Commands.CreateInvoice;
using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Application.Orders.Queries.GetOrderDiscrepancies;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository, IOrderValidationService, IPOProvider, IInvoiceSellerProvider
    {
        public readonly AppDbContext _context;

        public OrderRepository (AppDbContext context)
        {
            _context = context;
        }

        // Métodos de Escritura
        public async Task AddAsync(Order order)
        {
            // Esto inserta la orden y, por inferencia, todos los detalles que contenga la lista en memoria.
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateStatus(Order order)
        {
            // Solo marcamos la propiedad Status como modificada
            _context.Orders.Attach(order);
            _context.Entry(order).Property(o => o.Status).IsModified = true;

        }

        // Métodos de Lectura
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(p => p.Salesperson)
                .Include(s => s.Store)
                .Include(d => d.orderDetails)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            // FindAsync es eficiente, pero si necesitas Includes, usa FirstOrDefaultAsync
            return await _context.Orders
                .Include(p => p.Salesperson)
                .Include(s => s.Store)
                .Include(d => d.orderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(s => s.SalespersonId == userId)
                .Include(s => s.Store)
                .Include(d => d.orderDetails)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order?>> GetOrdersByStoreIdAsync(Guid storeId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(s => s.StoreId == storeId)
                .Include(p => p.Salesperson)
                .Include(d => d.orderDetails)
                .ToListAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
        }

        // Utilidades de Negocio

        public async Task<int> GetOrderCountByDateAsync(DateTime date)
        {
            // 1. Calculamos el inicio y el fin del día para evitar errores de traducción en Postgres
            var startOfDay = date.Date; // Ejemplo: 2026-03-17 00:00:00
            var endOfDay = startOfDay.AddDays(1); // Ejemplo: 2026-03-18 00:00:00

            // 2. Contamos las órdenes que caen exactamente en ese rango
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.CreatedAt >= startOfDay && o.CreatedAt < endOfDay)
                .CountAsync();
        }

        public async Task<string> GetPoByOrderId(Guid id)
        {
            // Buscamos en la tabla de facturas (Bills) aquella que pertenezca a la orden
            // y extraemos ÚNICAMENTE la columna POD usando Select() para máxima eficiencia.
            var pod = await _context.Orders
                .AsNoTracking()
                .Where(i => i.Id == id)
                .Select(i => i.PO)
                .FirstOrDefaultAsync();

            // Si la orden no tiene factura aún, o la factura no tiene POD, 
            // devolvemos un string vacío en lugar de null para evitar NullReferenceExceptions en tu frontend.
            return pod ?? string.Empty;
        }

        public async Task<User?> GetUserByOrderId(Guid id)
        {
            // Usamos Select para proyectar directamente la entidad relacionada (Salesperson).
            // Esto evita cargar las columnas de la tabla Order en la memoria de tu API.
            return await _context.Orders
                .AsNoTracking() // Es una consulta de solo lectura
                .Where(o => o.Id == id)
                .Select(o => o.Salesperson) // Extraemos únicamente al vendedor
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasAnyOrderWithRouteAsync(Guid routeId)
        {
            // .AnyAsync() es la forma más rápida de verificar existencia en SQL.
            // Se traduce a: SELECT EXISTS (SELECT 1 FROM "Orders" WHERE "RouteId" = @routeId)
            // No descarga ninguna fila a la memoria, solo devuelve true o false.
            return await _context.Orders
                .AsNoTracking()
                .AnyAsync(o => o.SalesRouteId == routeId);
        }
    }
}
