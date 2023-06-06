using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoAzureWebIII.Migrations
{
    /// <inheritdoc />
    public partial class creoNuevaMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mensaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    medioEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remitente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    destinatario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensaje", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensaje");
        }
    }
}
