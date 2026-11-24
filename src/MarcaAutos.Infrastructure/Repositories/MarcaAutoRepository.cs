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

        public async Task<IEnumerable<MarcaAuto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.MarcaAuto.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<MarcaAuto> CreateAsync(
            MarcaAuto request,
            CancellationToken cancellationToken
        )
        {
            _context.MarcaAuto.Add(request);
            await _context.SaveChangesAsync(cancellationToken);

            return request;
        }

        public async Task<MarcaAuto> GetById(int id, CancellationToken cancellationToken)
        {
            var data = await _context
                .MarcaAuto.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return data ?? throw new KeyNotFoundException($"MarcaAuto with ID {id} was not found.");
        }
    }
}
