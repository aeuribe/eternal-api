namespace eternal_api.Application.Identity.Interfaces
{
    public interface IIdentityRepository
    {
        // Agregamos string UserId al Tuple de retorno
        Task<(bool Succeeded, string? Token, string UserId, string[] Errors)> LoginAsync(string email, string password);

        Task<(bool Succeeded, string UserId, string[] Errors)> CreateUserAsync(string email, string password, string rol);

        Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<(bool Succeeded, string[] Errors)> UpdateUserStatusAsync(string userId, bool isEnabled);

        Task<(bool Succeeded, string[] Errors)> AdminUpdateUserPasswordAsync(string userId, string newPassword);

        Task<bool> EmailExistsAsync(string email);

        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword);
        Task<(bool Succeeded, string? Email, string? Role, string[] Errors)> GetUserAsync(string userId);
        Task DeleteUserAsync(string identityUserId);
    }
}
