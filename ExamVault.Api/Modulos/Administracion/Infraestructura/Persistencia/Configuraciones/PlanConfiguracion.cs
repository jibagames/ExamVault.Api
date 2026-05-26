using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class PlanConfiguracion : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Planes");
            builder.HasKey(p => p.IdPlanes);

            builder.Property(p => p.NombreDelPlan).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PrecioMensual).HasColumnType("decimal(18,2)");
        }
    }
}