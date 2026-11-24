using System.ComponentModel.DataAnnotations;
using MarcaAutos.Api.Validations.Attributes;

namespace MarcaAutos.Api.Dtos
{
    public class CreateMarcaAutoRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string CountryOrigin { get; set; }
        [GreatherThan(1900)]
        public int FoundedYear { get; set; }
    }
}
