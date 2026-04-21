using eternal_api.Domain.Entities;

namespace eternal_api.Application.Identity.Command.Register
{
    public interface ICityProvider
    {
        Task<City?> GetByIdAsync(Guid id);
    }
}
