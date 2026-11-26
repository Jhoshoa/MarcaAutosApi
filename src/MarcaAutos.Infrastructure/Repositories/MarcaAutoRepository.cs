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

        public async Task<MarcaAuto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context
                .MarcaAuto.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            //var marcaAuto = await GetByIdAsync(id, cancellationToken);
            var marcaAuto = await _context.MarcaAuto.FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken
            );

            if (marcaAuto is not null)
            {
                _context.MarcaAuto.Remove(marcaAuto);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
