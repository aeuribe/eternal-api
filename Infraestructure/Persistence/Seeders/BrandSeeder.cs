using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Persistence.Seeders
{
    public static class BrandSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // 1. Definimos las marcas que QUEREMOS tener
            var requiredBrandNames = new List<string> { "Eternal", "Valmy", "Pronto" };

            // 2. Buscamos cuáles de esas ya existen en la DB (en una sola consulta)
            var existingBrandNames = await context.Brands
                .Where(b => requiredBrandNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync();

            // 3. Filtramos para quedarnos solo con las que NO existen
            var brandsToInstall = requiredBrandNames
                .Where(name => !existingBrandNames.Contains(name))
                .Select(name => new Brand(name))
                .ToList();

            // 4. Si hay marcas nuevas, las insertamos
            if (brandsToInstall.Any())
            {
                await context.Brands.AddRangeAsync(brandsToInstall);
                await context.SaveChangesAsync();
            }
        }
    }
}