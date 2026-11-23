using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcaAutos.Domain;

namespace MarcaAutos.Application.Interfaces
{
    public interface IMarcaAutoService
    {
        Task<IEnumerable<MarcasAutos>> ObtenerTodasLasMarcasAsync();
    }
}
