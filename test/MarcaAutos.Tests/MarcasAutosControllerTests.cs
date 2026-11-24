using MarcaAutos.Api.Controllers;
using MarcaAutos.Api.Dtos;
using MarcaAutos.Application.Services;
using MarcaAutos.Domain;
using MarcaAutos.Infrastructure.Persistence;
using MarcaAutos.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Tests
{
    public class MarcasAutosControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithAllSeededCarBrands()
        {
            // Arrange
            await using var context = GetInMemoryDbContext();
            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAutoResponse>>(okResult.Value).ToList();

            Assert.Equal(4, marcas.Count);
            Assert.Contains(marcas, m => m.Name == "Toyota" && m.CountryOrigin == "Japon");
            Assert.Contains(marcas, m => m.Name == "Ford" && m.FoundedYear == 1903);
        }

        [Fact]
        public async Task GetAll_WhenNewBrandAdded_ShouldIncludeItInResponse()
        {
            // Arrange
            await using var context = GetInMemoryDbContext();
            context.MarcasAutos.Add(new MarcasAutos
            {
                Id = 5,
                Name = "Mazda",
                CountryOrigin = "Japón",
                FoundedYear = 1920
            });
            await context.SaveChangesAsync();

            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAutoResponse>>(okResult.Value).ToList();

            Assert.Equal(5, marcas.Count);
            Assert.Contains(marcas, m => m.Name == "Mazda");
        }
    }
}
