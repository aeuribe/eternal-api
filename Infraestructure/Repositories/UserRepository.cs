using eternal_api.Application.Identity.Command.Login;
using eternal_api.Application.Identity.Command.Register;
using eternal_api.Application.Orders.Queries.GetOrdersBySalespersonId;
using eternal_api.Application.Users.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository, ISalespersonValidationService, IUserIdentityProvider, IUserRegistrationService, ISellerProvider
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            // OJO AQUÍ: 
            // 1. Quitamos el .Include() porque para actualizar relaciones solo necesitamos cambiar el ID.
            // 2. Quitamos el .AsNoTracking() para que EF Core rastree la entidad y el Handler pueda mutarla.
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> ListAsync()
        {
            return await _context.Users
                .Include(u => u.SalesRoute)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<User?> GetByIdentityUser(string identityUserId)
        {
            return await _context.Users
                .Include(u => u.SalesRoute)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);
        }

        public async Task<(string Name, string LastName)> GetBasicProfileAsync(string identityUserId)
        {
            var user = await _context.Users
                .AsNoTracking() // 1. Apagamos el tracking (es solo lectura)
                .Where(u => u.IdentityUserId == identityUserId)
                .Select(u => new { u.Name, u.LastName }) // 2. Proyectamos solo lo necesario
                .FirstOrDefaultAsync();

            // 3. Retornamos los datos o valores por defecto para evitar errores de nulos
            return (user?.Name ?? "Usuario", user?.LastName ?? "Sin Apellido");
        }

        public async Task<int> GetSellersQuantity()
        {
            // Usamos CountAsync para que la suma la haga PostgreSQL y no el servidor de C#
            return await _context.Users
                .AsNoTracking() // 1. Rendimiento: No necesitamos trackear estas entidades
                .Where(u => u.Rol == "Vendedor" && u.IsActive) // 2. Filtramos solo vendedores activos
                .CountAsync();
        }
    }
}
