using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Configuraciones
{
    public class MonitorMateriaConfiguracion : IEntityTypeConfiguration<MonitorMateria>
    {
        public void Configure(EntityTypeBuilder<MonitorMateria> builder)
        {
            builder.ToTable("MonitoresMaterias");

            builder.HasKey(mm => new { mm.IdMonitor, mm.IdMateria });
        }
    }
}
