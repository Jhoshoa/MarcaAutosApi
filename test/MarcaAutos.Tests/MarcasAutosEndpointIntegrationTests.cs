using System.Net;
using System.Net.Http.Json;
using MarcaAutos.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MarcaAutos.Tests;

public sealed class MarcasAutosEndpointIntegrationTests : IDisposable
{
    private readonly ApiFactory _factory;
    private readonly HttpClient _client;

    public MarcasAutosEndpointIntegrationTests()
    {
        _factory = new ApiFactory();
        _client = _factory.CreateClient();
        _factory.InitializeDatabase();
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Fact]
    public async Task GetById_WhenIdIsNotAnInteger_ReturnsBadRequestValidationProblem()
    {
        using var response = await _client.GetAsync("/api/MarcasAutos/abc");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status);
        Assert.Contains("id", problem.Errors.Keys);
    }

    [Fact]
    public async Task Create_WhenRequestFailsValidation_ShouldReturnBadRequest()
    {
        // Arrange
        var requestBody = new CreateMarcaAutoRequest
        {
            Name = "Ford",
            CountryOrigin = "",
            FoundedYear = 1987,
        };

        // Act
        using var response = await _client.PostAsJsonAsync("/api/MarcasAutos", requestBody);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status);
    }

    [Fact]
    public async Task Create_WhenFoundedYearIsNotGreaterThanMinimum_ShouldReturnBadReqeuest()
    {
        // Arrage
        var requestBody = new CreateMarcaAutoRequest
        {
            Name = "Tesla",
            CountryOrigin = "United States",
            FoundedYear = 1900,
        };

        // Act
        using var response = await _client.PostAsJsonAsync("/api/MarcasAutos", requestBody);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Edit_WhenBrandExists_ShouldReturnUpdatedBrand()
    {
        var requestBody = new UpdateMarcaAutoRequest
        {
            Name = "Toyota Updated",
            CountryOrigin = "Japan",
            FoundedYear = 1937,
        };

        using var response = await _client.PutAsJsonAsync("/api/MarcasAutos/1", requestBody);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedBrand = await response.Content.ReadFromJsonAsync<MarcaAutoResponse>();

        Assert.NotNull(updatedBrand);
        Assert.Equal(1, updatedBrand.Id);
        Assert.Equal(requestBody.Name, updatedBrand.Name);
        Assert.Equal(requestBody.CountryOrigin, updatedBrand.CountryOrigin);
        Assert.Equal(requestBody.FoundedYear, updatedBrand.FoundedYear);

        using var getResponse = await _client.GetAsync("/api/MarcasAutos/1");
        var persistedBrand = await getResponse.Content.ReadFromJsonAsync<MarcaAutoResponse>();

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(persistedBrand);
        Assert.Equal(requestBody.Name, persistedBrand.Name);
    }

    [Fact]
    public async Task Edit_WhenBrandDoesNotExist_ShouldReturnNotFound()
    {
        var requestBody = new UpdateMarcaAutoRequest
        {
            Name = "Nonexistent",
            CountryOrigin = "Nowhere",
            FoundedYear = 2000,
        };

        using var response = await _client.PutAsJsonAsync("/api/MarcasAutos/999", requestBody);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Edit_WhenRequestFailsValidation_ShouldReturnBadRequest()
    {
        var requestBody = new UpdateMarcaAutoRequest
        {
            Name = "Toyota",
            CountryOrigin = "Japan",
            FoundedYear = 1800,
        };

        using var response = await _client.PutAsJsonAsync("/api/MarcasAutos/1", requestBody);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status);
    }

    [Fact]
    public async Task Delete_WhenIdIdNotExist_ShouldReturnNoContent()
    {
        // Act
        using var response = await _client.DeleteAsync("api/MarcasAutos/945");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenIdIdIsIncorrect_ShouldReturnBadRequest()
    {
        // Act
        using var response = await _client.DeleteAsync("api/MarcasAutos/abcd");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status);
    }
}
