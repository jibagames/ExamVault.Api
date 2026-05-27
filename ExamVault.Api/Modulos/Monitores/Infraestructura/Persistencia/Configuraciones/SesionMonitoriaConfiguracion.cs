using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Configuraciones
{
    public class SesionMonitoriaConfiguracion : IEntityTypeConfiguration<SesionMonitoria>
    {
        public void Configure(EntityTypeBuilder<SesionMonitoria> builder)
        {
            builder.ToTable("SesionesMonitoria");
            builder.HasKey(s => s.IdSesion);

            builder.Property(s => s.Estado).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Modalidad).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Ubicacion).HasMaxLength(255);
            builder.Property(s => s.SolicitadoEn).HasColumnType("timestamp with time zone");
            builder.Property(s => s.FechaProgramada).HasColumnType("timestamp with time zone");
        }
    }
}