using eternal_api.Application.Orders.Queries.GetOrderDiscrepancies;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Orders.Interfaces
{
    public interface IOrderRepository
    {
        // Escritura
        Task AddAsync(Order order);
        Task UpdateStatus(Order order);

        // Lectura
        Task<Order?> GetByIdAsync(Guid id);
        Task<bool> HasAnyOrderWithRouteAsync(Guid routeId);
        Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(Guid userId);
        Task<IEnumerable<Order?>> GetOrdersByStoreIdAsync(Guid storeId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task DeleteAsync(Order order);

        // Utilidades de Negocio
        Task<int> GetOrderCountByDateAsync(DateTime date);

    }
}
