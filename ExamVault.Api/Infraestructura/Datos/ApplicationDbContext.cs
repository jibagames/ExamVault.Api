using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Configuraciones;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Infraestructura.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<ProgramaMateria> ProgramasMaterias { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<TipoMaterial> TiposMateriales { get; set; }
        public DbSet<Descarga> Descargas { get; set; }
        public DbSet<Modulos.Monitores.Dominio.Entidades.Monitor> Monitores { get; set; }
        public DbSet<MonitorMateria> MonitoresMaterias { get; set; }
        public DbSet<SesionMonitoria> SesionesMonitores { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<PermisosAtributos> PermisosAtributos { get; set; }
        public DbSet<TipoMaterial> TipoMaterial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MaterialConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoMaterialConfiguracion());
            modelBuilder.ApplyConfiguration(new DescargaConfiguracion());
            modelBuilder.Entity<Institucion>().HasKey(e => e.IdInstituciones);
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new RolConfiguracion());
            modelBuilder.Entity<Programa>().HasKey(e => e.IdPrograma);
            modelBuilder.Entity<Materia>().HasKey(e => e.IdMateria);
            modelBuilder.Entity<Modulos.Monitores.Dominio.Entidades.Monitor>().HasKey(e => e.IdMonitor);
            modelBuilder.Entity<SesionMonitoria>().HasKey(e => e.IdSesion);
            modelBuilder.Entity<Calificacion>().HasKey(e => e.IdCalificacion);
            modelBuilder.Entity<Plan>().HasKey(e => e.IdPlanes);
            modelBuilder.Entity<Suscripcion>().HasKey(e => e.IdSuscripciones);
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguracion());
            modelBuilder.Entity<ProgramaMateria>().HasKey(pm => new { pm.IdPrograma, pm.IdMateria });
            modelBuilder.Entity<MonitorMateria>().HasKey(mm => new { mm.IdMonitor, mm.IdMateria });
            modelBuilder.ApplyConfiguration(new PermisosConfiguracion());
            modelBuilder.ApplyConfiguration(new PermisosAtributosConfiguracion());

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(u => u.IdInstituciones);
            });

            modelBuilder.Entity<Modulos.Monitores.Dominio.Entidades.Monitor>(entity =>
            {
                entity.HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdUsuario);
            });

            modelBuilder.Entity<Programa>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(p => p.IdInstituciones);
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(m => m.IdInstituciones);
            });

            modelBuilder.Entity<TipoMaterial>(entity =>
            {
                entity.HasKey(e => e.IdTipoMaterial);
                entity.Property(e => e.NombreMaterial).HasMaxLength(100);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);
                entity.Property(e => e.Titulo).HasMaxLength(255);
                entity.Property(e => e.Estado).HasMaxLength(50);
                entity.Property(e => e.UrlArchivo).HasMaxLength(500);
                entity.Property(e => e.FechaModeracion).HasColumnType("timestamp with time zone");

                entity.HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdUsuario);
                entity.HasOne<Materia>().WithMany().HasForeignKey(m => m.IdMateria);
                entity.HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdModerador).IsRequired(false);
                entity.HasOne<TipoMaterial>().WithMany().HasForeignKey(m => m.IdTipoMaterial);
            });

            modelBuilder.Entity<Descarga>(entity =>
            {
                entity.Property(e => e.FechaDescarga).HasColumnType("timestamp with time zone");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(d => d.IdUsuario);
                entity.HasOne<TipoMaterial>().WithMany().HasForeignKey(d => d.IdMaterial);
            });

            modelBuilder.Entity<SesionMonitoria>(entity =>
            {
                entity.Property(e => e.SolicitadoEn).HasColumnType("timestamp with time zone");
                entity.Property(e => e.FechaProgramada).HasColumnType("timestamp with time zone");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(s => s.IdUsuario);
                entity.HasOne<Modulos.Monitores.Dominio.Entidades.Monitor>().WithMany().HasForeignKey(s => s.IdMonitor);
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.Property(e => e.CreadoEn).HasColumnType("timestamp with time zone");
                entity.HasOne<SesionMonitoria>().WithMany().HasForeignKey(c => c.IdSesion);
            });

            modelBuilder.Entity<Suscripcion>(entity =>
            {
                entity.Property(e => e.FechaInicio).HasColumnType("date");
                entity.Property(e => e.FechaFin).HasColumnType("date");
                entity.HasOne<Plan>().WithMany().HasForeignKey(s => s.IdPlanes);
                entity.HasOne<Institucion>().WithMany().HasForeignKey(s => s.IdInstituciones);
            });

            modelBuilder.Entity<Auditoria>(entity =>
            {
                entity.HasKey(e => e.IdAuditoria);
                entity.Property(e => e.FechaAccion).HasColumnType("timestamp with time zone").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(a => a.IdUsuario);
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.ToTable("Permisos");
                entity.HasOne<Rol>().WithMany().HasForeignKey(c => c.IdRol).HasConstraintName("PERMISOS_ROLES_FK").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PermisosAtributos>(entity =>
            {
                entity.ToTable("PermisosAtributos");
                entity.HasOne<Rol>().WithMany().HasForeignKey(c => c.IdRol).HasConstraintName("PERMISOS_ATRIBUTOS_ROLES_FK").OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}