using eternal_api.Application.Presentations.Interfaces;
using eternal_api.Domain.Entities;
using eternal_api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eternal_api.Infraestructure.Repositories
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly AppDbContext _context;

        public PresentationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Presentation presentation)
        {
            await _context.Presentations.AddAsync(presentation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Presentation presentation)
        {
            _context.Presentations.Update(presentation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Presentation presentation)
        {
            if (presentation is null) return false;

            _context.Presentations.Remove(presentation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Presentation?> GetByIdAsync(Guid id)
        {
            return await _context.Presentations
                .Include(p => p.Family)
                    .ThenInclude(f => f.Brand)
                .Include(p => p.Family)
                    .ThenInclude(f => f.Class)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Presentation>> ListAsync()
        {
            return await _context.Presentations
                .Include(p => p.Family)
                    .ThenInclude(f => f.Brand)
                .Include(p => p.Family)
                    .ThenInclude(f => f.Class)
                .AsNoTracking() // Optimiza la memoria en el servidor para las listas
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Presentations.AnyAsync(p => p.Id == id);
        }

        public async Task<Presentation?> GetByGenericCodeAsync(string genericCode)
        {
            // Buscamos la primera presentación que coincida con el UPC/Código Genérico
            // Usamos FirstOrDefaultAsync para que devuelva null si no encuentra nada
            return await _context.Presentations
                .FirstOrDefaultAsync(p => p.GenericCode == genericCode);
        }
    }
}