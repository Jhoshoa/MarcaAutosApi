using MarcaAutos.Application.Interfaces;
using MarcaAutos.Application.Models;
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

        public async Task<IReadOnlyList<MarcaAutoResult>> ObtenerTodasLasMarcasAsync(
            CancellationToken cancellationToken
        )
        {
            var marcasAutos = await _repository.GetAllAsync(cancellationToken);
            return marcasAutos.Select(Map).ToList();
        }

        public async Task<MarcaAutoResult> CrearMarcaAutoAsync(
            CreateMarcaAutoCommand command,
            CancellationToken cancellationToken
        )
        {
            var marcaAuto = new MarcaAuto
            {
                Name = command.Name,
                CountryOrigin = command.CountryOrigin,
                FoundedYear = command.FoundedYear,
            };

            var createdMarcaAuto = await _repository.CreateAsync(marcaAuto, cancellationToken);
            return Map(createdMarcaAuto);
        }

        public async Task<MarcaAutoResult?> ObtenerMarcaAutoPorIdAsync(
            int id,
            CancellationToken cancellationToken
        )
        {
            var marcaAuto = await _repository.GetByIdAsync(id, cancellationToken);
            return marcaAuto is null ? null : Map(marcaAuto);
        }

        public async Task BorrarMarcaAutoAsync(int id, CancellationToken cancellationToken)
        {
            await _repository.DeleteByIdAsync(id, cancellationToken);
        }

        private static MarcaAutoResult Map(MarcaAuto marcaAuto)
        {
            return new MarcaAutoResult(
                marcaAuto.Id,
                marcaAuto.Name,
                marcaAuto.CountryOrigin,
                marcaAuto.FoundedYear
            );
        }
    }
}
