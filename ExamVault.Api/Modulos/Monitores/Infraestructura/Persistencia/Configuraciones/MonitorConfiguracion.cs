using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Configuraciones
{
    public class MonitorConfiguracion : IEntityTypeConfiguration<Dominio.Entidades.Monitor>
    {
        public void Configure(EntityTypeBuilder<Dominio.Entidades.Monitor> builder)
        {
            builder.ToTable("Monitores");
            builder.HasKey(m => m.IdMonitor);

            builder.Property(m => m.Disponibilidad).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Presentacion).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.IdUsuario).IsRequired();
        }
    }
}