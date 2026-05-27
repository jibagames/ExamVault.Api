using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Configuraciones
{
    public class CalificacionConfiguracion : IEntityTypeConfiguration<Calificacion>
    {
        public void Configure(EntityTypeBuilder<Calificacion> builder)
        {
            builder.ToTable("Calificaciones");
            builder.HasKey(c => c.IdCalificacion);

            builder.Property(c => c.Estrellas).IsRequired();
            builder.Property(c => c.Comentario).HasMaxLength(500);
            builder.Property(c => c.CreadoEn).HasColumnType("timestamp with time zone");

            builder.HasOne<SesionMonitoria>()
                   .WithMany()
                   .HasForeignKey(c => c.IdSesion)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}