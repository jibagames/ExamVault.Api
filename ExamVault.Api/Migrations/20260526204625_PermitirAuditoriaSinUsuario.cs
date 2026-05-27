using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class PermitirAuditoriaSinUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_IdUsuario",
                table: "Auditorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_TiposMateriales_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_TiposMateriales_IdTipoMaterial",
                table: "Materiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposMateriales",
                table: "TiposMateriales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias");

            migrationBuilder.RenameTable(
                name: "TiposMateriales",
                newName: "TipoMaterial");

            migrationBuilder.RenameTable(
                name: "Auditorias",
                newName: "Auditoria");

            migrationBuilder.RenameIndex(
                name: "IX_Auditorias_IdUsuario",
                table: "Auditoria",
                newName: "IX_Auditoria_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoMaterial",
                table: "TipoMaterial",
                column: "IdTipoMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditoria",
                table: "Auditoria",
                column: "IdAuditoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_Usuarios_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Descargas_TipoMaterial_IdMaterial",
                table: "Descargas",
                column: "IdMaterial",
                principalTable: "TipoMaterial",
                principalColumn: "IdTipoMaterial",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_TipoMaterial_IdTipoMaterial",
                table: "Materiales",
                column: "IdTipoMaterial",
                principalTable: "TipoMaterial",
                principalColumn: "IdTipoMaterial",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_Usuarios_IdUsuario",
                table: "Auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_TipoMaterial_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_TipoMaterial_IdTipoMaterial",
                table: "Materiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoMaterial",
                table: "TipoMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditoria",
                table: "Auditoria");

            migrationBuilder.RenameTable(
                name: "TipoMaterial",
                newName: "TiposMateriales");

            migrationBuilder.RenameTable(
                name: "Auditoria",
                newName: "Auditorias");

            migrationBuilder.RenameIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditorias",
                newName: "IX_Auditorias_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposMateriales",
                table: "TiposMateriales",
                column: "IdTipoMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditorias",
                table: "Auditorias",
                column: "IdAuditoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_IdUsuario",
                table: "Auditorias",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Descargas_TiposMateriales_IdMaterial",
                table: "Descargas",
                column: "IdMaterial",
                principalTable: "TiposMateriales",
                principalColumn: "IdTipoMaterial",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_TiposMateriales_IdTipoMaterial",
                table: "Materiales",
                column: "IdTipoMaterial",
                principalTable: "TiposMateriales",
                principalColumn: "IdTipoMaterial",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
