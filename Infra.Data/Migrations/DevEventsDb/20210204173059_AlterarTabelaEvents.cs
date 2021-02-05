using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations.DevEventsDb
{
    public partial class AlterarTabelaEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoriaId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoriaId",
                table: "Events",
                column: "CategoriaId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoriaId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Events",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoriaId",
                table: "Events",
                column: "CategoriaId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
