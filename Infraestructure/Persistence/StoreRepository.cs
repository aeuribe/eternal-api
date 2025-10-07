using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Persistence
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task<Store?> GetByIdAsync(Guid id) =>
            await _context.Stores.FindAsync(id);

        public async Task<Store?> GetByNameAsync(string name) =>
            await _context.Stores.FirstOrDefaultAsync(s => s.Name == name);

        public async Task<List<Store>> GetByCityAsync(Guid cityId) =>
            await _context.Stores.Where(s => s.CityId == cityId).ToListAsync();

        public async Task<List<Store>> ListAsync() =>
            await _context.Stores.ToListAsync();
    }

}
