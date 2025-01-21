using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoraAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EspEntities",
                columns: table => new
                {
                    idESP = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspEntities", x => x.idESP);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TemperaturaDados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    temperatura = table.Column<double>(type: "double", nullable: false),
                    data = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EspEntityidESP = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperaturaDados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperaturaDados_EspEntities_EspEntityidESP",
                        column: x => x.EspEntityidESP,
                        principalTable: "EspEntities",
                        principalColumn: "idESP",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TemperaturaDados_EspEntityidESP",
                table: "TemperaturaDados",
                column: "EspEntityidESP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperaturaDados");

            migrationBuilder.DropTable(
                name: "EspEntities");
        }
    }
}
