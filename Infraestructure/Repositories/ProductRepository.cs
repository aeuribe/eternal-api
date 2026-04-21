using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Application.Products.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository, IPresentationProductValidationService
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Brand)
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Class)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            // FindAsync no soporta Includes, usamos FirstOrDefaultAsync
            return await _context.Products
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Brand)
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Class)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByBrandAsync(Guid brandId)
        {
            return await _context.Products
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Brand)
                .Include(p => p.Presentation)
                    .ThenInclude(pr => pr.Family)
                        .ThenInclude(f => f.Class)
                // Filtramos navegando por la jerarquía hasta el ID de la marca
                .Where(p => p.Presentation.Family.BrandId == brandId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> HasAnyProductAssociatedAsync(Guid presentationId)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.PresentationId == presentationId);
        }

        public async Task<bool> HasAnyProductWithOrdersAssociatedAsync(Guid presentationId)
        {
            return await _context.OrderDetails
                .AsNoTracking()
                .AnyAsync(od => od.Product.PresentationId == presentationId);
        }

        public async Task<bool> IsInActivePlanogramAsync(Guid productId)
        {
            // Hacemos un JOIN manual entre Distributions y Planograms
            return await _context.Distributions
                .Where(d => d.ProductId == productId)
                .Join(
                    _context.Planograms,
                    dist => dist.PlanogramId, // Clave en Distribution
                    plan => plan.Id,          // Clave en Planogram
                    (dist, plan) => plan      // Nos quedamos con el Planograma
                )
                .AnyAsync(p => p.isActive);   // Verificamos si alguno está activo
        }

        public async Task<bool> HasPlanogramsWithOrdersAsync(Guid productId)
        {
            // Suponiendo que tienes un _context.Orders y que cada Order tiene un PlanogramId
            return await _context.Distributions
                .Where(d => d.ProductId == productId)
                .Join(
                    _context.Orders,
                    dist => dist.PlanogramId, // Clave en Distribution
                    order => order.PlanogramId, // Clave en Order
                    (dist, order) => order    // Nos quedamos con la Orden
                )
                .AnyAsync(); // Si devuelve true, es porque encontró al menos una orden
        }
    }
}