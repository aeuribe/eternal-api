using eternal_api.Domain.Entities;

namespace eternal_api.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> ListAsync();
    }
}
