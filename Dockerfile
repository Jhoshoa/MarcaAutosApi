FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project files while preserving the src/ directory structure.
COPY ["src/MarcaAutos.Api/MarcaAutos.Api.csproj", "src/MarcaAutos.Api/"]
COPY ["src/MarcaAutos.Infrastructure/MarcaAutos.Infrastructure.csproj", "src/MarcaAutos.Infrastructure/"]
COPY ["src/MarcaAutos.Application/MarcaAutos.Application.csproj", "src/MarcaAutos.Application/"]
COPY ["src/MarcaAutos.Domain/MarcaAutos.Domain.csproj", "src/MarcaAutos.Domain/"]
RUN dotnet restore "src/MarcaAutos.Api/MarcaAutos.Api.csproj"

# Copy the remaining source files and publish the application.
COPY . .
WORKDIR "/src/src/MarcaAutos.Api"
RUN dotnet publish "MarcaAutos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MarcaAutos.Api.dll"]
