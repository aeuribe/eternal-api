using eternal_api.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace eternal_api.Application.Assignments.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<Guid> AddAsync(Assignment visitLog);
        Task<Assignment> GetAssignmentByIdAsync(Guid id);
        Task<bool> UpdateAsync(Assignment visitLog);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Assignment>> ListAsync();

        // Vital para el CreateOrderHandler: ¿A qué ruta pertenece esta tienda hoy?
        Task<Assignment?> GetByStoreIdAsync(Guid storeId);
    }
}

