using eternal_api.Application.Cities.Interfaces;
using eternal_api.Application.Identity.Command.Register;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class CityRepository : ICityRepository, ICityProvider
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }

        public async Task<City?> GetByIdAsync(Guid id) =>
            await _context.Cities.FindAsync(id);

        public async Task<City?> GetByNameAsync(string name) =>
            await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);

        public async Task<List<City>> ListAsync() =>
            await _context.Cities.ToListAsync();

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Cities.AnyAsync(o => o.Id == id);
        }
    }


}
