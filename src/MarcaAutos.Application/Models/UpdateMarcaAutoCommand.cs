using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarcaAutos.Application.Models
{
    public class UpdateMarcaAutoCommand
    {
        public UpdateMarcaAutoCommand(string name, string countryOrigin, int foundedYear)
        {
            Name = name;
            CountryOrigin = countryOrigin;
            FoundedYear = foundedYear;
        }

        public string Name { get; }
        public string CountryOrigin { get; }
        public int FoundedYear { get; }
    }
}
