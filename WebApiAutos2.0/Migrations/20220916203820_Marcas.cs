using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutos2.Migrations
{
    public partial class Marcas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoId",
                table: "Autos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marcas_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autos_AutoId",
                table: "Autos",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_AutoId",
                table: "Marcas",
                column: "AutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Autos_AutoId",
                table: "Autos",
                column: "AutoId",
                principalTable: "Autos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Autos_AutoId",
                table: "Autos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Autos_AutoId",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "AutoId",
                table: "Autos");
        }
    }
}
