
using eternal_api.Application.Identity.Interfaces;
using eternal_api.Infraestructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace eternal_api.Infraestructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService; // Asegúrate que la interfaz se llame ITokenRepository

        public IdentityRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        // Este es el método que usará tu LoginHandler
        public async Task<(bool Succeeded, string? Token, string UserId, string[] Errors)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, null, string.Empty, ["Usuario no encontrado."]);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, null, string.Empty, ["Contraseña incorrecta."]);

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateJwtToken(user.Id, user.Email!, roles);

            // Retornamos el user.Id junto con el token
            return (true, token, user.Id, []);
        }

        public async Task<(bool Succeeded, string UserId, string[] Errors)> CreateUserAsync(string email, string password, string rol)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return (false, string.Empty, result.Errors.Select(e => e.Description).ToArray());

            await _userManager.AddToRoleAsync(user, rol);

            return (true, user.Id, []);
        }

        public async Task<(bool Succeeded, string[] Errors)> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, ["Usuario no encontrado."]);

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUserStatusAsync(string userId, bool isEnabled)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return (false, ["Usuario no encontrado."]);

            await _userManager.SetLockoutEnabledAsync(user, !isEnabled);

            if (!isEnabled)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }

            return (true, []);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null; // Si es distinto de null, el email ya está ocupado
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return string.Empty;

            // Genera un token único (Base64) vinculado a este usuario
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, ["El usuario no existe."]);

            // Valida el token y cambia la contraseña sin pedir la anterior
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, []);
        }

        public async Task<(bool Succeeded, string? Email, string? Role, string[] Errors)> GetUserAsync(string userId)
        {
            // 1. Buscar al usuario
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, null, null, new[] { "Usuario no encontrado." });
            }

            // 2. Obtener sus roles (Identity maneja una lista, tomamos el primero para este caso)
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); // Ojo: Si el usuario puede tener varios roles, ajusta esto.

            // 3. Retornar éxito
            return (true, user.Email, role, Array.Empty<string>());
        }

        public async Task DeleteUserAsync(string identityUserId)
        {
            var user = await _userManager.FindByIdAsync(identityUserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<(bool Succeeded, string[] Errors)> AdminUpdateUserPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, new[] { "Usuario no encontrado" });
            }

            // 1. Generamos el token de reseteo internamente
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // 2. Aplicamos la nueva contraseña usando ese token
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}