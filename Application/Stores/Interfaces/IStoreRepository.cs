using eternal_api.Domain.Entities;

namespace eternal_api.Application.Stores.Interfaces
{
    public interface IStoreRepository
    {
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);

        // Búsquedas individuales
        Task<Store?> GetByIdAsync(Guid id);
        Task<Store?> GetByNameAsync(string name);
        Task<Store?> GetByStoreNumberAsync(string storeNumber); // Nuevo: Búsqueda por número de tienda

        // Búsquedas por agrupación
        Task<List<Store>> GetByCityAsync(Guid cityId);
        Task<List<Store>> GetByDistrictAsync(Guid districtId);  // Nuevo: Búsqueda por distrito geográfico

        // Listados generales y validaciones
        Task<List<Store>> ListAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}