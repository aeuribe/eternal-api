using Microsoft.AspNetCore.Identity;

namespace eternal_api.Infraestructure.Persistence.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            // Ajusta la lista de roles a los que realmente uses en tu aplicación
            string[] roles = { "Admin", "Vendedor", "USER" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new InvalidOperationException($"No se pudo crear el rol '{role}': {errors}");
                    }
                }
            }
        }
    }
}