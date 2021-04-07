using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations.DevEventsDb
{
    public partial class RelacionamentoNparaN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UsuarioId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UsuarioId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "EventModelUserModel",
                columns: table => new
                {
                    EventosId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModelUserModel", x => new { x.EventosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Events_EventosId",
                        column: x => x.EventosId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Users_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventModelUserModel_UsuariosId",
                table: "EventModelUserModel",
                column: "UsuariosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventModelUserModel");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_UsuarioId",
                table: "Events",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UsuarioId",
                table: "Events",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
