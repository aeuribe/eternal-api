using eternal_api.Domain.Entities;
using eternal_api.Domain.Enums; // <-- Importante para usar StateUsEnum
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Persistence.Seeders
{
    public static class CitySeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Solo insertamos si la tabla está vacía
            if (!await context.Cities.AnyAsync())
            {
                // Usamos el nuevo constructor: Name, StateUsEnum
                var cities = new List<City>
                {
                    new City("Miami", StateUsEnum.FL),
                    new City("Los Angeles", StateUsEnum.CA),
                    new City("Houston", StateUsEnum.TX)
                };

                await context.Cities.AddRangeAsync(cities);
                await context.SaveChangesAsync();
            }
        }
    }
}