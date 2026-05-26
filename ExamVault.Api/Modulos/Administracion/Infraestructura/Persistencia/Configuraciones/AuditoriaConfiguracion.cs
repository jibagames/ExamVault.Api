using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class AuditoriaConfiguracion : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("Auditorias");
            builder.HasKey(a => a.IdAuditoria);

            builder.Property(a => a.AccionAuditada).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Detalle).IsRequired().HasMaxLength(1000);
            builder.Property(a => a.IpOrigen).IsRequired().HasMaxLength(45); // Soporte para IPv6
            builder.Property(a => a.FechaAccion).IsRequired();
        }
    }
}