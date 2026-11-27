using MarcaAutos.Application.Models;

namespace MarcaAutos.Application.Interfaces
{
    public interface IMarcaAutoService
    {
        Task<IReadOnlyList<MarcaAutoResult>> ObtenerTodasLasMarcasAsync(
            CancellationToken cancellationToken
        );
        Task<MarcaAutoResult> CrearMarcaAutoAsync(
            CreateMarcaAutoCommand command,
            CancellationToken cancellationToken
        );
        Task<MarcaAutoResult?> ObtenerMarcaAutoPorIdAsync(
            int id,
            CancellationToken cancellationToken
        );
        Task BorrarMarcaAutoAsync(int id, CancellationToken cancellationToken);
        Task<MarcaAutoResult?> EditarMarcaAutoAsync(
            int id,
            UpdateMarcaAutoCommand data,
            CancellationToken cancellationToken
        );
    }
}
