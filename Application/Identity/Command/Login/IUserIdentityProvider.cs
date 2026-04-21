namespace eternal_api.Application.Identity.Command.Login
{
    public interface IUserIdentityProvider
    {
        Task<(string Name, string LastName)> GetBasicProfileAsync(string identityUserId);
    }
}
