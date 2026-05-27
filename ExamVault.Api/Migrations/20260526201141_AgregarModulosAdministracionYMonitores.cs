using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarModulosAdministracionYMonitores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_SesionesMonitores_IdSesion",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_TipoMaterial_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_TipoMaterial_IdTipoMaterial",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Usuarios_IdModerador",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Usuarios_IdUsuario",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Instituciones_IdInstituciones",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Programas_Instituciones_IdInstituciones",
                table: "Programas");

            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "SesionesMonitores");

            migrationBuilder.DropTable(
                name: "TipoMaterial");

            migrationBuilder.AlterColumn<string>(
                name: "SegundoNombre",
                table: "Usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimerNombre",
                table: "Usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FotoUrl",
                table: "Usuarios",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContrasenaHash",
                table: "Usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Usuarios",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Suscripciones",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreRol",
                table: "Roles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombrePrograma",
                table: "Programas",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Programas",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioMensual",
                table: "Planes",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "NombreDelPlan",
                table: "Planes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Permiso",
                table: "PermisosAtributos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreTabla",
                table: "PermisosAtributos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreAtributo",
                table: "PermisosAtributos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Permiso",
                table: "Permisos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreTabla",
                table: "Permisos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Presentacion",
                table: "Monitores",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Disponibilidad",
                table: "Monitores",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Materias",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Materias",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NombreInstitucion",
                table: "Instituciones",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Instituciones",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DominioCorreo",
                table: "Instituciones",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "Calificaciones",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    AccionAuditada = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Detalle = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IpOrigen = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK_Auditorias_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionesMonitoria",
                columns: table => new
                {
                    IdSesion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SolicitadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaProgramada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modalidad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Ubicacion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdMonitor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMonitoria", x => x.IdSesion);
                    table.ForeignKey(
                        name: "FK_SesionesMonitoria_Monitores_IdMonitor",
                        column: x => x.IdMonitor,
                        principalTable: "Monitores",
                        principalColumn: "IdMonitor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMonitoria_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposMateriales",
                columns: table => new
                {
                    IdTipoMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreMaterial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMateriales", x => x.IdTipoMaterial);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramasMaterias_IdMateria",
                table: "ProgramasMaterias",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_DominioCorreo",
                table: "Instituciones",
                column: "DominioCorreo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_IdUsuario",
                table: "Auditorias",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitoria_IdMonitor",
                table: "SesionesMonitoria",
                column: "IdMonitor");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitoria_IdUsuario",
                table: "SesionesMonitoria",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_SesionesMonitoria_IdSesion",
                table: "Calificaciones",
                column: "IdSesion",
                principalTable: "SesionesMonitoria",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Usuarios_IdModerador",
                table: "Materiales",
                column: "IdModerador",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Usuarios_IdUsuario",
                table: "Materiales",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Instituciones_IdInstituciones",
                table: "Materias",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Programas_Instituciones_IdInstituciones",
                table: "Programas",
                column: "IdInstituciones",
                principalTable: "Instituciones",
                principalColumn: "IdInstituciones",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramasMaterias_Materias_IdMateria",
                table: "ProgramasMaterias",
                column: "IdMateria",
                principalTable: "Materias",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramasMaterias_Programas_IdPrograma",
                table: "ProgramasMaterias",
                column: "IdPrograma",
                principalTable: "Programas",
                principalColumn: "IdPrograma",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_SesionesMonitoria_IdSesion",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_Materiales_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Descargas_TiposMateriales_IdMaterial",
                table: "Descargas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_TiposMateriales_IdTipoMaterial",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Usuarios_IdModerador",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materiales_Usuarios_IdUsuario",
                table: "Materiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Instituciones_IdInstituciones",
                table: "Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Programas_Instituciones_IdInstituciones",
                table: "Programas");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramasMaterias_Materias_IdMateria",
                table: "ProgramasMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramasMaterias_Programas_IdPrograma",
                table: "ProgramasMaterias");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "SesionesMonitoria");

            migrationBuilder.DropTable(
                name: "TiposMateriales");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_ProgramasMaterias_IdMateria",
                table: "ProgramasMaterias");

            migrationBuilder.DropIndex(
                name: "IX_Instituciones_DominioCorreo",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "SegundoNombre",
                table: "Usuarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimerNombre",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FotoUrl",
                table: "Usuarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Correo",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ContrasenaHash",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Usuarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Suscripciones",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NombreRol",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NombrePrograma",
                table: "Programas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Programas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioMensual",
                table: "Planes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreDelPlan",
                table: "Planes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Permiso",
                table: "PermisosAtributos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NombreTabla",
                table: "PermisosAtributos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NombreAtributo",
                table: "PermisosAtributos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Permiso",
                table: "Permisos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NombreTabla",
                table: "Permisos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Presentacion",
                table: "Monitores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Disponibilidad",
                table: "Monitores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Materias",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Materias",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "NombreInstitucion",
                table: "Instituciones",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Instituciones",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DominioCorreo",
                table: "Instituciones",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "Calificaciones",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccionAuditada = table.Column<string>(type: "text", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: false),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK_Auditoria_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionesMonitores",
                columns: table => new
                {
                    IdSesion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FechaProgramada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdMonitor = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Modalidad = table.Column<string>(type: "text", nullable: false),
                    SolicitadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMonitores", x => x.IdSesion);
                    table.ForeignKey(
                        name: "FK_SesionesMonitores_Monitores_IdMonitor",
                        column: x => x.IdMonitor,
                        principalTable: "Monitores",
                        principalColumn: "IdMonitor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMonitores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoMaterial",
                columns: table => new
                {
                    IdTipoMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreMaterial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMaterial", x => x.IdTipoMaterial);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitores_IdMonitor",
                table: "SesionesMonitores",
                column: "IdMonitor");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitores_IdUsuario",
                table: "SesionesMonitores",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_SesionesMonitores_IdSesion",
                table: "Calificaciones",
                column: "IdSesion",
                principalTable: "SesionesMonitores",
                principalColumn: "IdSesion",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Usuarios_IdModerador",
                table: "Materiales",
                column: "IdModerador",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiales_Usuarios_IdUsuario",
                table: "Materiales",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
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
        }
    }
}
