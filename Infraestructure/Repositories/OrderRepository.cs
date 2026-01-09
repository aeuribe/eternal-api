using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly AppDbContext _context;

        public OrderRepository (AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(p => p.Salesperson)
                .Include(s => s.Store)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Where(s => s.SalespersonId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order?>> GetOrdersByStoreIdAsync(Guid storeId)
        {
            return await _context.Orders
                .Where(s => s.StoreId == storeId)
                .ToListAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
