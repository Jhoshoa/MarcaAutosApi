using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarcaAutos.Domain
{
    public class MarcasAutos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOrigin { get; set; }
        public int FoundedYear { get; set; }
    }
}