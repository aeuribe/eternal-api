// En eternal_api.Application.Areas.Interfaces.IAreaRepository
using eternal_api.Domain.Entities;

public interface IAreaRepository
{
    Task AddAsync(Area area);
    Task<IEnumerable<Area>> GetAllAsync();
    Task<Area?> GetByIdAsync(Guid id);

    // Lo nuevo
    Task UpdateAsync(Area area);
    Task DeleteAsync(Area area);
    Task<bool> HasRegionsAsync(Guid areaId); // Validación vital para no romper la BD
}