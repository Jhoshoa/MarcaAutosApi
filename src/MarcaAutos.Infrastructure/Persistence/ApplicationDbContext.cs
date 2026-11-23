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
    }
}
