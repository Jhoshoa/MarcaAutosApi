using MarcaAutos.Application.Interfaces;
using MarcaAutos.Application.Services;
using MarcaAutos.Infrastructure.Persistence;
using MarcaAutos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMarcaAutoRepository, MarcaAutoRepository>();
builder.Services.AddScoped<IMarcaAutoService, MarcaAutoService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
