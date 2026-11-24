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

        public async Task<IEnumerable<MarcaAuto>> GetAllAsync()
        {
            return await _context.MarcaAuto.AsNoTracking().ToListAsync();
        }

        public async Task<MarcaAuto> CreateAsync(MarcaAuto request)
        {
            _context.MarcaAuto.Add(request);
            await _context.SaveChangesAsync();

            return request;
        }
    }
}
