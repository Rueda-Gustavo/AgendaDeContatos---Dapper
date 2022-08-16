using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaDeContatos.Infra.Migrations
{
    public partial class AdicionandoCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Família" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 2, "Trabalho" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 3, "Escola" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
