FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar los archivos .csproj respetando la estructura src/
COPY ["src/MarcaAutos.Api/MarcaAutos.Api.csproj", "src/MarcaAutos.Api/"]
COPY ["src/MarcaAutos.Infrastructure/MarcaAutos.Infrastructure.csproj", "src/MarcaAutos.Infrastructure/"]
COPY ["src/MarcaAutos.Application/MarcaAutos.Application.csproj", "src/MarcaAutos.Application/"]
COPY ["src/MarcaAutos.Domain/MarcaAutos.Domain.csproj", "src/MarcaAutos.Domain/"]
RUN dotnet restore "src/MarcaAutos.Api/MarcaAutos.Api.csproj"

# Copiar el resto del código fuente y publicar
COPY . .
WORKDIR "/src/src/MarcaAutos.Api"
RUN dotnet publish "MarcaAutos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MarcaAutos.Api.dll"]
