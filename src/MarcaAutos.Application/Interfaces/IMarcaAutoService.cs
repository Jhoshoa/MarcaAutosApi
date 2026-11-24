using MarcaAutos.Domain;

namespace MarcaAutos.Application.Interfaces
{
    public interface IMarcaAutoService
    {
        Task<IEnumerable<MarcaAuto>> ObtenerTodasLasMarcasAsync(
            CancellationToken cancellationToken
        );
        Task<MarcaAuto> CrearMarcaAutoAsync(MarcaAuto request, CancellationToken cancellationToken);
        Task<MarcaAuto> ObtenerMarcaAutoPorId(int id, CancellationToken cancellationToken);
    }
}
