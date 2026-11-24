using MarcaAutos.Domain;

namespace MarcaAutos.Application.Interfaces
{
    public interface IMarcaAutoService
    {
        Task<IEnumerable<MarcaAuto>> ObtenerTodasLasMarcasAsync();
        Task<MarcaAuto> CrearMarcaAutoAsync(MarcaAuto request);
    }
}
