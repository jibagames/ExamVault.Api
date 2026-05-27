using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;
using ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;
using ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Configuraciones;
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
        public DbSet<TipoMaterial> TipoMaterial { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InstitucionConfiguracion());
            modelBuilder.ApplyConfiguration(new PlanConfiguracion());
            modelBuilder.ApplyConfiguration(new AuditoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new MateriaConfiguracion());
            modelBuilder.ApplyConfiguration(new ProgramaConfiguracion());
            modelBuilder.ApplyConfiguration(new ProgramaMateriaConfiguracion());
            modelBuilder.ApplyConfiguration(new SuscripcionConfiguracion());

            modelBuilder.ApplyConfiguration(new MaterialConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoMaterialConfiguracion());
            modelBuilder.ApplyConfiguration(new DescargaConfiguracion());

            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new RolConfiguracion());
            modelBuilder.ApplyConfiguration(new PermisosConfiguracion());
            modelBuilder.ApplyConfiguration(new PermisosAtributosConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioRolConfiguracion());

            modelBuilder.ApplyConfiguration(new MonitorConfiguracion());
            modelBuilder.ApplyConfiguration(new MonitorMateriaConfiguracion());
            modelBuilder.ApplyConfiguration(new SesionMonitoriaConfiguracion());
            modelBuilder.ApplyConfiguration(new CalificacionConfiguracion());

            modelBuilder.Entity<Usuario>().HasOne<Institucion>().WithMany().HasForeignKey(u => u.IdInstituciones);
            modelBuilder.Entity<Modulos.Monitores.Dominio.Entidades.Monitor>().HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdUsuario);
            modelBuilder.Entity<Programa>().HasOne<Institucion>().WithMany().HasForeignKey(p => p.IdInstituciones);
            modelBuilder.Entity<Materia>().HasOne<Institucion>().WithMany().HasForeignKey(m => m.IdInstituciones);

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
  
            modelBuilder.Entity<Auditoria>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(a => a.IdUsuario)
                .IsRequired(false);

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