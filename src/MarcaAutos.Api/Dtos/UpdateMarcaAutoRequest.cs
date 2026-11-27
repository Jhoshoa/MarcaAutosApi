using System.ComponentModel.DataAnnotations;
using MarcaAutos.Api.Validations.Attributes;

namespace MarcaAutos.Api.Dtos
{
    public class UpdateMarcaAutoRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CountryOrigin { get; set; } = string.Empty;

        [GreaterThan(1900)]
        public int FoundedYear { get; set; }
    }
}
