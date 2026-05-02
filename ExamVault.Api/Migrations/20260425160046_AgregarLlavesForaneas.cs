using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarLlavesForaneas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Suscripciones",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Suscripciones",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdInstituciones",
                table: "Usuarios",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_IdInstituciones",
                table: "Suscripciones",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_IdPlanes",
                table: "Suscripciones",
                column: "IdPlanes");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitores_IdMonitor",
                table: "SesionesMonitores",
                column: "IdMonitor");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitores_IdUsuario",
                table: "SesionesMonitores",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Programas_IdInstituciones",
                table: "Programas",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_IdInstituciones",
                table: "Materias",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdMateria",
                table: "Materiales",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Descargas_IdMaterial",
                table: "Descargas",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Descargas_IdUsuario",
                table: "Descargas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdSesion",
                table: "Calificaciones",
                column: "IdSesion");

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_Usuarios_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_SesionesMonitores_IdSesion",
                table: "Calificaciones",
                column: "IdSesion",
                principalTable: "SesionesMonitores",
                principalColumn: "IdSesion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Descargas_Materiales_IdMaterial",
                table: "Descargas",
                column: "IdMaterial",
                principalTable: "Materiales",
                principalColumn: "IdMaterial",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Descargas_Usuarios_IdUsuario",
                table: "Descargas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Materias_IdMateria",
                table: "Materiales",
                column: "IdMateria",
                principalTable: "Materias",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Instituciones_IdInstituciones",
                table: "Materias",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programas_Instituciones_IdInstituciones",
                table: "Programas",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SesionesMonitores_Monitores_IdMonitor",
                table: "SesionesMonitores",
                column: "IdMonitor",
                principalTable: "Monitores",
                principalColumn: "IdMonitor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SesionesMonitores_Usuarios_IdUsuario",
                table: "SesionesMonitores",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Instituciones_IdInstituciones",
                table: "Suscripciones",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Planes_IdPlanes",
                table: "Suscripciones",
                column: "IdPlanes",
                principalTable: "Planes",
                principalColumn: "IdPlanes",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Instituciones_IdInstituciones",
                table: "Usuarios",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_Usuarios_IdUsuario",
                table: "Auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_SesionesMonitores_IdSesion",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_Materiales_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_Usuarios_IdUsuario",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Materias_IdMateria",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Instituciones_IdInstituciones",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Programas_Instituciones_IdInstituciones",
                table: "Programas");

            migrationBuilder.DropForeignKey(
                name: "FK_SesionesMonitores_Monitores_IdMonitor",
                table: "SesionesMonitores");

            migrationBuilder.DropForeignKey(
                name: "FK_SesionesMonitores_Usuarios_IdUsuario",
                table: "SesionesMonitores");

            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Instituciones_IdInstituciones",
                table: "Suscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Planes_IdPlanes",
                table: "Suscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Instituciones_IdInstituciones",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdInstituciones",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_IdInstituciones",
                table: "Suscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_IdPlanes",
                table: "Suscripciones");

            migrationBuilder.DropIndex(
                name: "IX_SesionesMonitores_IdMonitor",
                table: "SesionesMonitores");

            migrationBuilder.DropIndex(
                name: "IX_SesionesMonitores_IdUsuario",
                table: "SesionesMonitores");

            migrationBuilder.DropIndex(
                name: "IX_Programas_IdInstituciones",
                table: "Programas");

            migrationBuilder.DropIndex(
                name: "IX_Materias_IdInstituciones",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materiales_IdMateria",
                table: "Materiales");

            migrationBuilder.DropIndex(
                name: "IX_Descargas_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropIndex(
                name: "IX_Descargas_IdUsuario",
                table: "Descargas");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_IdSesion",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditoria");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Suscripciones",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Suscripciones",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
