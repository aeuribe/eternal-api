using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        public readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetByIdAsync(Guid id) =>
           await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category) =>
           await _context.Products
                         .Where(p => p.Category == category)
                         .ToListAsync();
    }
}
