using eternal_api.Domain.Entities;

namespace eternal_api.Application.Cities.Interfaces
{
    public interface ICityRepository
    {
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task<City?> GetByIdAsync(Guid id);
        Task<City?> GetByNameAsync(string name);
        Task<bool> ExistsAsync(Guid id);
        Task<List<City>> ListAsync();
    }
}
