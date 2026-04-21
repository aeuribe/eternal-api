using eternal_api.Domain.Entities;

namespace eternal_api.Application.Products.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product); // Este hará el borrado físico (context.Products.Remove)
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByBrandAsync(Guid brandId);

        // Para validar el Delete Físico
        Task<bool> HasPlanogramsWithOrdersAsync(Guid productId);

        // Para validar el Desactivar
        Task<bool> IsInActivePlanogramAsync(Guid productId);
    }
}