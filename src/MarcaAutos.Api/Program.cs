using MarcaAutos.Application.Interfaces;
using MarcaAutos.Application.Services;
using MarcaAutos.Infrastructure.Persistence;
using MarcaAutos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("----------------------->" + args[0]);
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddScoped<IMarcaAutoRepository, MarcaAutoRepository>();
        builder.Services.AddScoped<IMarcaAutoService, MarcaAutoService>();

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddProblemDetails();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseExceptionHandler();

        //app.UseStatusCodePages();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // Apply pending migrations in Development before serving requests, including migration-based seed data.
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
