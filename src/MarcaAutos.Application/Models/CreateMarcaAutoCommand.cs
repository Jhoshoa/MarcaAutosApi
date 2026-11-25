namespace MarcaAutos.Application.Models
{
    public sealed class CreateMarcaAutoCommand
    {
        public CreateMarcaAutoCommand(string name, string countryOrigin, int foundedYear)
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
