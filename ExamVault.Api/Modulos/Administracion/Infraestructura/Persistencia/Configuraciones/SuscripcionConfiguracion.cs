using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class SuscripcionConfiguracion : IEntityTypeConfiguration<Suscripcion>
    {
        public void Configure(EntityTypeBuilder<Suscripcion> builder)
        {
            builder.ToTable("Suscripciones");
            builder.HasKey(s => s.IdSuscripciones);

            builder.Property(s => s.Estado).IsRequired().HasMaxLength(50);
            builder.Property(s => s.FechaInicio).IsRequired();
            builder.Property(s => s.FechaFin).IsRequired();

            builder.HasOne<Plan>()
                   .WithMany()
                   .HasForeignKey(s => s.IdPlanes);

            builder.HasOne<Institucion>()
                   .WithMany()
                   .HasForeignKey(s => s.IdInstituciones);
        }
    }
}