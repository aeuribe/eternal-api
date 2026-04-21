using eternal_api.Domain.Entities;

namespace eternal_api.Application.Districts.Interfaces
{
    public interface IDistrictRepository
    {
        Task AddAsync(District district);
        Task UpdateAsync(District district);
        Task DeleteAsync(District district);
        Task<IEnumerable<District>> GetAllAsync();
        Task<District?> GetByIdAsync(Guid id);
        Task<bool> HasStoresAsync(Guid districtId);
    }
}
