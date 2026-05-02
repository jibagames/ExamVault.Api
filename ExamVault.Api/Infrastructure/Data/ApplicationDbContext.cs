using ExamVault.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamVault.Api.Infrastructure.Data
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
        public DbSet<Descarga> Descargas { get; set; }
        public DbSet<Domain.Entities.Monitor> Monitores { get; set; }
        public DbSet<MonitorMateria> MonitoresMaterias { get; set; }
        public DbSet<SesionMonitoria> SesionesMonitores { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Suscripcion> Suscripciones { get; set; }
        public DbSet<Auditoria> Auditoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Institucion>().HasKey(e => e.IdInstituciones);
            modelBuilder.Entity<Usuario>().HasKey(e => e.IdUsuario);
            modelBuilder.Entity<Rol>().HasKey(e => e.IdRol);
            modelBuilder.Entity<Programa>().HasKey(e => e.IdPrograma);
            modelBuilder.Entity<Materia>().HasKey(e => e.IdMateria);
            modelBuilder.Entity<Descarga>().HasKey(e => e.IdDescarga);
            modelBuilder.Entity<Domain.Entities.Monitor>().HasKey(e => e.IdMonitor);
            modelBuilder.Entity<SesionMonitoria>().HasKey(e => e.IdSesion);
            modelBuilder.Entity<Calificacion>().HasKey(e => e.IdCalificacion);
            modelBuilder.Entity<Plan>().HasKey(e => e.IdPlanes);
            modelBuilder.Entity<Suscripcion>().HasKey(e => e.IdSuscripciones);
            modelBuilder.Entity<UsuarioRol>().HasKey(ur => new { ur.IdUsuario, ur.IdRol });
            modelBuilder.Entity<ProgramaMateria>().HasKey(pm => new { pm.IdPrograma, pm.IdMateria });
            modelBuilder.Entity<MonitorMateria>().HasKey(mm => new { mm.IdMonitor, mm.IdMateria });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(u => u.IdInstituciones);
            });

            modelBuilder.Entity<Domain.Entities.Monitor>(entity =>
            {
                entity.HasOne<Domain.Entities.Monitor>().WithMany().HasForeignKey(m => m.IdUsuario);
            });

            modelBuilder.Entity<Programa>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(p => p.IdInstituciones);
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasOne<Institucion>().WithMany().HasForeignKey(m => m.IdInstituciones);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);
                entity.Property(e => e.FechaModeracion).HasColumnType("timestamp with time zone");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdUsuario);
                entity.HasOne<Materia>().WithMany().HasForeignKey(m => m.IdMateria);
                entity.HasOne<Usuario>().WithMany().HasForeignKey(m => m.IdModerador).IsRequired(false);
            });

            modelBuilder.Entity<Descarga>(entity =>
            {
                entity.Property(e => e.FechaDescarga).HasColumnType("timestamp with time zone");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(d => d.IdUsuario);
                entity.HasOne<Material>().WithMany().HasForeignKey(d => d.IdMaterial);
            });

            modelBuilder.Entity<SesionMonitoria>(entity =>
            {
                entity.Property(e => e.SolicitadoEn).HasColumnType("timestamp with time zone");
                entity.Property(e => e.FechaProgramada).HasColumnType("timestamp with time zone");
                entity.HasOne<Usuario>().WithMany().HasForeignKey(s => s.IdUsuario);
                entity.HasOne<Domain.Entities.Monitor>().WithMany().HasForeignKey(s => s.IdMonitor);
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
        }
    }
}