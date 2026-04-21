using eternal_api.Domain.Entities;

namespace eternal_api.Application.Identity.Command.Register
{
    public interface IUserRegistrationService
    {
        Task AddAsync(User user);
    }
}
