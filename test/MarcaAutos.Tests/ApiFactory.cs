using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MarcaAutos.Tests;

public sealed class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Avoid the Development-only PostgreSQL migrations during integration tests.
        builder.UseEnvironment("Testing");
    }
}
