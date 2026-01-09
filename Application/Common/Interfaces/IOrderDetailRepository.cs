using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task AddAsync(OrderDetail orderDetail);
        Task UpdateAsync(OrderDetail orderDetail);
        Task<OrderDetail?> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderDetail>> GetAllByOrderIdAsync(Guid orderId);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(OrderDetail orderDetail);
    }
}
