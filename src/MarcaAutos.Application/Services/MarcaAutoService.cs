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

        public async Task<IEnumerable<MarcaAuto>> ObtenerTodasLasMarcasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MarcaAuto> CrearMarcaAutoAsync(MarcaAuto request)
        {
            return await _repository.CreateAsync(request);
        }
    }
}
