using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarcaAutos.Domain;

namespace MarcaAutos.Application.Interfaces
{
    public interface IMarcaAutoRepository
    {
        Task<IEnumerable<MarcaAuto>> GetAllAsync(CancellationToken cancellationToken);
        Task<MarcaAuto> CreateAsync(MarcaAuto request, CancellationToken cancellationToken);
    }
}
