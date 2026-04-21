using eternal_api.Domain.Entities;

namespace eternal_api.Application.Brands.Interfaces
{
    public interface IBrandRepository
    {
        Task AddAsyncBrand(Brand brand);
        Task UpdateAsync(Brand brand);
        Task<Brand?> GetByIdAsync(Guid id);
        Task<Brand?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Brand>> ListAsync();

    }
}
