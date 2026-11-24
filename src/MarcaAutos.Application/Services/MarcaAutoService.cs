using MarcaAutos.Application.Interfaces;
using MarcaAutos.Domain;

namespace MarcaAutos.Application.Services
{
    public class MarcaAutoService : IMarcaAutoService
    {
        private readonly IMarcaAutoRepository _repository;

        public MarcaAutoService(IMarcaAutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MarcaAuto>> ObtenerTodasLasMarcasAsync(
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<MarcaAuto> CrearMarcaAutoAsync(
            MarcaAuto request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.CreateAsync(request, cancellationToken);
        }

        public async Task<MarcaAuto> ObtenerMarcaAutoPorId(
            int id,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetById(id, cancellationToken);
        }
    }
}
