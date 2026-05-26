using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class ProgramaConfiguracion : IEntityTypeConfiguration<Programa>
    {
        public void Configure(EntityTypeBuilder<Programa> builder)
        {
            builder.ToTable("Programas");
            builder.HasKey(p => p.IdPrograma);

            builder.Property(p => p.NombrePrograma).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Descripcion).HasMaxLength(500);

            builder.HasOne<Institucion>()
                   .WithMany()
                   .HasForeignKey(p => p.IdInstituciones)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}