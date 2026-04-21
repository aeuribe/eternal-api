using eternal_api.Domain.Entities;

namespace eternal_api.Application.Regions.Interfaces
{
    public interface IRegionRepository
    {
        Task AddAsync(Region region);
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);

        Task UpdateAsync(Region region);
        Task DeleteAsync(Region region);
        Task<bool> HasDistrictsAsync(Guid regionId); // Protección contra borrado
    }
}
