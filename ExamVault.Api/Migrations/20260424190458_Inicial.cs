using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    AccionAuditada = table.Column<string>(type: "text", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.IdAuditoria);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estrellas = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdSesion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.IdCalificacion);
                });

            migrationBuilder.CreateTable(
                name: "Descargas",
                columns: table => new
                {
                    IdDescarga = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaDescarga = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descargas", x => x.IdDescarga);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreInstitucion = table.Column<string>(type: "text", nullable: false),
                    DominioCorreo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Creado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.IdInstituciones);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "Monitores",
                columns: table => new
                {
                    IdMonitor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Disponibilidad = table.Column<string>(type: "text", nullable: false),
                    Presentacion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitores", x => x.IdMonitor);
                });

            migrationBuilder.CreateTable(
                name: "MonitoresMaterias",
                columns: table => new
                {
                    IdMonitor = table.Column<int>(type: "integer", nullable: false),
                    IdMateria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoresMaterias", x => new { x.IdMonitor, x.IdMateria });
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    IdPlanes = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreDelPlan = table.Column<string>(type: "text", nullable: false),
                    LimiteAlmacenamiento = table.Column<int>(type: "integer", nullable: false),
                    PrecioMensual = table.Column<decimal>(type: "numeric", nullable: false),
                    LimiteEstudiantes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.IdPlanes);
                });

            migrationBuilder.CreateTable(
                name: "Programas",
                columns: table => new
                {
                    IdPrograma = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombrePrograma = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programas", x => x.IdPrograma);
                });

            migrationBuilder.CreateTable(
                name: "ProgramasMaterias",
                columns: table => new
                {
                    IdPrograma = table.Column<int>(type: "integer", nullable: false),
                    IdMateria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramasMaterias", x => new { x.IdPrograma, x.IdMateria });
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreRol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "SesionesMonitores",
                columns: table => new
                {
                    IdSesion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    SolicitadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaProgramada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modalidad = table.Column<string>(type: "text", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdMonitor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMonitores", x => x.IdSesion);
                });

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    IdSuscripciones = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    IdPlanes = table.Column<int>(type: "integer", nullable: false),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => x.IdSuscripciones);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimerNombre = table.Column<string>(type: "text", nullable: false),
                    SegundoNombre = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    ContrasenaHash = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    Contacto = table.Column<string>(type: "text", nullable: true),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.IdUsuario, x.IdRol });
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    UrlArchivo = table.Column<string>(type: "text", nullable: false),
                    TamanoBytes = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdMateria = table.Column<int>(type: "integer", nullable: false),
                    IdModerador = table.Column<int>(type: "integer", nullable: true),
                    FechaModeracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.IdMaterial);
                    table.ForeignKey(
                        name: "FK_Materiales_Usuarios_IdModerador",
                        column: x => x.IdModerador,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_Materiales_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdModerador",
                table: "Materiales",
                column: "IdModerador");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdUsuario",
                table: "Materiales",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Descargas");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Monitores");

            migrationBuilder.DropTable(
                name: "MonitoresMaterias");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Programas");

            migrationBuilder.DropTable(
                name: "ProgramasMaterias");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SesionesMonitores");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
