using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaDeContatos.Infra.Migrations
{
    public partial class AjustandoCampoContatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Contatos");

            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Contatos",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
