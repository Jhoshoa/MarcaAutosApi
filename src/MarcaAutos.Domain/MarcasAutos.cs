using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarcaAutos.Domain
{
    public class MarcasAutos
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryOrigin { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
    }
}
