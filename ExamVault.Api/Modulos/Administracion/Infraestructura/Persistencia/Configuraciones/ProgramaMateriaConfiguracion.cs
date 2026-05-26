using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class ProgramaMateriaConfiguracion : IEntityTypeConfiguration<ProgramaMateria>
    {
        public void Configure(EntityTypeBuilder<ProgramaMateria> builder)
        {
            builder.ToTable("ProgramasMaterias");

            builder.HasKey(pm => new { pm.IdPrograma, pm.IdMateria });

            builder.HasOne<Programa>()
                   .WithMany()
                   .HasForeignKey(pm => pm.IdPrograma);

            builder.HasOne<Materia>()
                   .WithMany()
                   .HasForeignKey(pm => pm.IdMateria);
        }
    }
}