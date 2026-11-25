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
            var result = await controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert
                .IsAssignableFrom<IEnumerable<MarcaAutoResponse>>(okResult.Value)
                .ToList();

            Assert.Equal(4, marcas.Count);
            Assert.Contains(marcas, m => m.Name == "Toyota" && m.CountryOrigin == "Japon");
            Assert.Contains(marcas, m => m.Name == "Ford" && m.FoundedYear == 1903);
        }

        [Fact]
        public async Task GetAll_WhenNewBrandAdded_ShouldIncludeItInResponse()
        {
            // Arrange
            await using var context = GetInMemoryDbContext();
            context.MarcaAuto.Add(
                new MarcaAuto
                {
                    Id = 5,
                    Name = "Mazda",
                    CountryOrigin = "Japón",
                    FoundedYear = 1920,
                }
            );
            await context.SaveChangesAsync();

            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);

            // Act
            var result = await controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marcas = Assert
                .IsAssignableFrom<IEnumerable<MarcaAutoResponse>>(okResult.Value)
                .ToList();

            Assert.Equal(5, marcas.Count);
            Assert.Contains(marcas, m => m.Name == "Mazda");
        }

        [Fact]
        public async Task GetById_WhenBrandExists_ShouldReturnOkWithBrandResponse()
        {
            await using var context = GetInMemoryDbContext();
            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);

            var result = await controller.GetById(1, CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var marca = Assert.IsType<MarcaAutoResponse>(okResult.Value);
            Assert.Equal(1, marca.Id);
            Assert.Equal("Toyota", marca.Name);
        }

        [Fact]
        public async Task GetById_WhenBrandDoesNotExist_ShouldReturnNotFound()
        {
            await using var context = GetInMemoryDbContext();
            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);

            var result = await controller.GetById(999, CancellationToken.None);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtGetByIdWithGeneratedId()
        {
            await using var context = GetInMemoryDbContext();
            var repository = new MarcaAutoRepository(context);
            var service = new MarcaAutoService(repository);
            var controller = new MarcasAutosController(service);
            var request = new CreateMarcaAutoRequest
            {
                Name = "Mazda",
                CountryOrigin = "Japón",
                FoundedYear = 1920,
            };

            var result = await controller.Create(request, CancellationToken.None);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<MarcaAutoResponse>(createdResult.Value);
            Assert.Equal(nameof(MarcasAutosController.GetById), createdResult.ActionName);
            Assert.Equal(response.Id, createdResult.RouteValues!["id"]);
            Assert.Equal("Mazda", response.Name);
            Assert.NotEqual(0, response.Id);
        }
    }
}
