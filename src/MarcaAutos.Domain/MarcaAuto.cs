namespace MarcaAutos.Domain
{
    public class MarcaAuto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryOrigin { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
    }
}
