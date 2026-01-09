using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order?>> GetOrdersByUserIdAsync(Guid userId);
        Task<IEnumerable<Order?>> GetOrdersByStoreIdAsync(Guid storeId);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Order order);

    }
}
