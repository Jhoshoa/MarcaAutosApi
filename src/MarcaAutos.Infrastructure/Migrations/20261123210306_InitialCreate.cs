using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MarcaAutos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcasAutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryOrigin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FoundedYear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasAutos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MarcasAutos",
                columns: new[] { "Id", "CountryOrigin", "FoundedYear", "Name" },
                values: new object[,]
                {
                    { 1, "Japon", 1937, "Toyota" },
                    { 2, "Estados Unidos", 1903, "Ford" },
                    { 3, "Alemania", 1937, "Volkswagen" },
                    { 4, "Italia", 1939, "Ferrari" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcasAutos");
        }
    }
}
