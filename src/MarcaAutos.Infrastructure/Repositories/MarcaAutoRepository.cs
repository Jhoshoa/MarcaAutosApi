using MarcaAutos.Application.Interfaces;
using MarcaAutos.Domain;
using MarcaAutos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Infrastructure.Repositories
{
    public class MarcaAutoRepository : IMarcaAutoRepository
    {
        private readonly ApplicationDbContext _context;

        public MarcaAutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MarcasAutos>> GetAllAsync()
        {
            return await _context.MarcasAutos.AsNoTracking().ToListAsync();
        }
    }
}
