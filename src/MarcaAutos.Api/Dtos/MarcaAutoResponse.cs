namespace MarcaAutos.Api.Dtos
{
    public sealed class MarcaAutoResponse
    {
        public MarcaAutoResponse(int id, string name, string countryOrigin, int foundedYear)
        {
            Id = id;
            Name = name;
            CountryOrigin = countryOrigin;
            FoundedYear = foundedYear;
        }

        public int Id { get; }
        public string Name { get; }
        public string CountryOrigin { get; }
        public int FoundedYear { get; }
    }
}
