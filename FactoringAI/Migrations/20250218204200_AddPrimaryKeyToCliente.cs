using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoringAI.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyToCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingresos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deudas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PuntajeCredito = table.Column<int>(type: "int", nullable: false),
                    Aprobado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
