using MarcaAutos.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<MarcasAutos> MarcasAutos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MarcasAutos>(entity =>
            {
                entity.ToTable("MarcasAutos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CountryOrigin).IsRequired().HasMaxLength(100);

                entity.HasData(
                    new MarcasAutos { Id = 1, Name = "Toyota", CountryOrigin = "Japon", FoundedYear = 1937},
                    new MarcasAutos { Id = 2, Name = "Ford", CountryOrigin = "Estados Unidos", FoundedYear = 1903},
                    new MarcasAutos { Id = 3, Name = "Volkswagen", CountryOrigin = "Alemania", FoundedYear = 1937},
                    new MarcasAutos { Id = 4, Name = "Ferrari", CountryOrigin = "Italia", FoundedYear = 1939 }
                );
            });
        }
    }
}
