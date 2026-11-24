# MarcaAutos API

A .NET 10 REST API for retrieving vehicle-brand data, backed by PostgreSQL and Entity Framework Core.

## Quick start

Start PostgreSQL and the API with:

```bash
docker compose up -d
```

On startup in Development, the API waits for PostgreSQL, applies pending EF Core migrations, and inserts the seeded brands. Open Swagger at <http://localhost:8080/swagger> or call `GET /api/MarcasAutos` at <http://localhost:8080/api/MarcasAutos>.

Use `docker compose up --build -d` when you want to rebuild the API image as well as start the services.

## Run the application

### Docker Compose

Prerequisite: Docker Compose v2.

Start the API and PostgreSQL:

```bash
docker compose up -d
```

Compose waits until PostgreSQL is healthy before starting the API. In Development, the API applies pending migrations before serving requests; the initial migration inserts Toyota, Ford, Volkswagen, and Ferrari. For production, apply migrations through a controlled deployment step rather than application startup.

- API: <http://localhost:8080>
- Swagger UI: <http://localhost:8080/swagger>
- Endpoint: `GET /api/MarcasAutos`

Try the endpoint with curl:

```bash
curl http://localhost:8080/api/MarcasAutos
```

### Run the API locally

The API's local launch profile uses `http://localhost:5057`. Start PostgreSQL, then run the API from the repository root:

```bash
dotnet run --project src/MarcaAutos.Api/MarcaAutos.Api.csproj
```

- API: <http://localhost:5057>
- Swagger UI: <http://localhost:5057/swagger>

In Development, the API applies pending migrations at startup whether it runs locally or in Compose. The local API connects through `localhost:5432`; the containerized API connects to `postgres_db:5432` on the Compose network. When both APIs are running, they use the same database.

## Tests and coverage

Run the xUnit test suite from the repository root:

```bash
dotnet test test/MarcaAutos.Tests/MarcaAutos.Tests.csproj
```

Collect Cobertura coverage with Coverlet:

```bash
rm -rf test/MarcaAutos.Tests/TestResults coveragereport
dotnet test test/MarcaAutos.Tests/MarcaAutos.Tests.csproj --collect:"XPlat Code Coverage"
```

The Cobertura report is written under `test/MarcaAutos.Tests/TestResults/`. The
repository's `coverlet.runsettings` excludes generated EF Core migrations and
`Program` from coverage. No minimum coverage percentage is currently enforced.

To generate an HTML report, install ReportGenerator once:

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

Then run this from the repository root:

```bash
reportgenerator \
	-reports:"test/MarcaAutos.Tests/TestResults/**/coverage.cobertura.xml" \
	-targetdir:"coveragereport" \
	-reporttypes:Html
```

Open `coveragereport/index.html` to view the report.

## API

### Get all vehicle brands

`GET /api/MarcasAutos` returns the persisted brands with HTTP `200 OK`. Each item contains `id`, `name`, `countryOrigin`, and `foundedYear`. Swagger UI is enabled in the Development environment.

## Project structure

The solution is organized into application layers:

```text
MarcaAutosApi/
|-- src/
|   |-- MarcaAutos.Domain/          # Core vehicle-brand entity
|   |-- MarcaAutos.Application/     # Service and repository interfaces and service logic
|   |-- MarcaAutos.Infrastructure/  # EF Core context, PostgreSQL repository, and migrations
|   `-- MarcaAutos.Api/              # HTTP controller, Swagger, and dependency injection
|-- test/
|   `-- MarcaAutos.Tests/            # xUnit tests using EF Core InMemory
|-- Dockerfile                       # Multi-stage .NET 10 container build
`-- docker-compose.yml               # PostgreSQL 16 and API services
```

## Configuration and data

The default connection string in `src/MarcaAutos.Api/appsettings.json` targets PostgreSQL at `localhost:5432`. Docker Compose overrides it so the API container connects to the `postgres_db` service. The Compose database uses a named volume, so its data survives container restarts. The credentials in the checked-in configuration are for local development only; use managed secrets for deployed environments.

Stop the containers while preserving database data:

```bash
docker compose down
```

To also delete the database volume and its data:

```bash
docker compose down -v
```
