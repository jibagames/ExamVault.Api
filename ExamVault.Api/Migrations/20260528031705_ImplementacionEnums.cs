using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExamVault.Api.Migrations
{
    /// <inheritdoc />
    public partial class ImplementacionEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreInstitucion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DominioCorreo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Creado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.IdInstituciones);
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
                    NombreDelPlan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LimiteAlmacenamiento = table.Column<int>(type: "integer", nullable: false),
                    PrecioMensual = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    LimiteEstudiantes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.IdPlanes);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreRol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
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
                name: "Materias",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IdMateria);
                    table.ForeignKey(
                        name: "FK_Materias_Instituciones_IdInstituciones",
                        column: x => x.IdInstituciones,
                        principalTable: "Instituciones",
                        principalColumn: "IdInstituciones",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programas",
                columns: table => new
                {
                    IdPrograma = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombrePrograma = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programas", x => x.IdPrograma);
                    table.ForeignKey(
                        name: "FK_Programas_Instituciones_IdInstituciones",
                        column: x => x.IdInstituciones,
                        principalTable: "Instituciones",
                        principalColumn: "IdInstituciones",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrimerNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SegundoNombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Apellidos = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContrasenaHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Contacto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Instituciones_IdInstituciones",
                        column: x => x.IdInstituciones,
                        principalTable: "Instituciones",
                        principalColumn: "IdInstituciones",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    IdSuscripciones = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IdPlanes = table.Column<int>(type: "integer", nullable: false),
                    IdInstituciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => x.IdSuscripciones);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Instituciones_IdInstituciones",
                        column: x => x.IdInstituciones,
                        principalTable: "Instituciones",
                        principalColumn: "IdInstituciones",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Planes_IdPlanes",
                        column: x => x.IdPlanes,
                        principalTable: "Planes",
                        principalColumn: "IdPlanes",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdRol = table.Column<int>(type: "integer", nullable: false),
                    NombreTabla = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Permiso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.IdPermiso);
                    table.ForeignKey(
                        name: "PERMISOS_ROLES_FK",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermisosAtributos",
                columns: table => new
                {
                    IdPermisoAtributo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdRol = table.Column<int>(type: "integer", nullable: false),
                    NombreTabla = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NombreAtributo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Permiso = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosAtributos", x => x.IdPermisoAtributo);
                    table.ForeignKey(
                        name: "PERMISOS_ATRIBUTOS_ROLES_FK",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_ProgramasMaterias_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramasMaterias_Programas_IdPrograma",
                        column: x => x.IdPrograma,
                        principalTable: "Programas",
                        principalColumn: "IdPrograma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    IdAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaAccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccionAuditada = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Detalle = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IpOrigen = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK_Auditoria_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Materiales",
                columns: table => new
                {
                    IdMaterial = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlArchivo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TamanoBytes = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdMateria = table.Column<int>(type: "integer", nullable: false),
                    IdModerador = table.Column<int>(type: "integer", nullable: true),
                    FechaModeracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdTipoMaterial = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiales", x => x.IdMaterial);
                    table.ForeignKey(
                        name: "FK_Materiales_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materiales_TipoMaterial_IdTipoMaterial",
                        column: x => x.IdTipoMaterial,
                        principalTable: "TipoMaterial",
                        principalColumn: "IdTipoMaterial",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materiales_Usuarios_IdModerador",
                        column: x => x.IdModerador,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materiales_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Monitores",
                columns: table => new
                {
                    IdMonitor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Disponibilidad = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Presentacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitores", x => x.IdMonitor);
                    table.ForeignKey(
                        name: "FK_Monitores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Descargas_Materiales_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Materiales",
                        principalColumn: "IdMaterial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Descargas_TipoMaterial_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "TipoMaterial",
                        principalColumn: "IdTipoMaterial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Descargas_Usuarios_IdUsuario",
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
                name: "Calificaciones",
                columns: table => new
                {
                    IdCalificacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estrellas = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdSesion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.IdCalificacion);
                    table.ForeignKey(
                        name: "FK_Calificaciones_SesionesMonitoria_IdSesion",
                        column: x => x.IdSesion,
                        principalTable: "SesionesMonitoria",
                        principalColumn: "IdSesion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_IdSesion",
                table: "Calificaciones",
                column: "IdSesion");

            migrationBuilder.CreateIndex(
                name: "IX_Descargas_IdMaterial",
                table: "Descargas",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Descargas_IdUsuario",
                table: "Descargas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_DominioCorreo",
                table: "Instituciones",
                column: "DominioCorreo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdMateria",
                table: "Materiales",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdModerador",
                table: "Materiales",
                column: "IdModerador");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdTipoMaterial",
                table: "Materiales",
                column: "IdTipoMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Materiales_IdUsuario",
                table: "Materiales",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_IdInstituciones",
                table: "Materias",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_IdUsuario",
                table: "Monitores",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_IdRol",
                table: "Permisos",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosAtributos_IdRol",
                table: "PermisosAtributos",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Programas_IdInstituciones",
                table: "Programas",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramasMaterias_IdMateria",
                table: "ProgramasMaterias",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitoria_IdMonitor",
                table: "SesionesMonitoria",
                column: "IdMonitor");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMonitoria_IdUsuario",
                table: "SesionesMonitoria",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_IdInstituciones",
                table: "Suscripciones",
                column: "IdInstituciones");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_IdPlanes",
                table: "Suscripciones",
                column: "IdPlanes");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdInstituciones",
                table: "Usuarios",
                column: "IdInstituciones");
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
                name: "MonitoresMaterias");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "PermisosAtributos");

            migrationBuilder.DropTable(
                name: "ProgramasMaterias");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "SesionesMonitoria");

            migrationBuilder.DropTable(
                name: "Materiales");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Programas");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Monitores");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "TipoMaterial");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Instituciones");
        }
    }
}
