using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIdUsuarioToMonitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Monitores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_IdUsuario",
                table: "Monitores",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitores_Monitores_IdUsuario",
                table: "Monitores",
                column: "IdUsuario",
                principalTable: "Monitores",
                principalColumn: "IdMonitor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Monitores_IdUsuario",
                table: "Monitores");

            migrationBuilder.DropIndex(
                name: "IX_Monitores_IdUsuario",
                table: "Monitores");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Monitores");
        }
    }
}
