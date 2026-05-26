using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class MateriaConfiguracion : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("Materias");
            builder.HasKey(m => m.IdMateria);

            builder.Property(m => m.Nombre).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Codigo).IsRequired().HasMaxLength(20);

            builder.HasOne<Institucion>()
                   .WithMany()
                   .HasForeignKey(m => m.IdInstituciones)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}