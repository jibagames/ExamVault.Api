using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Configuraciones
{
    public class TipoMaterialConfiguracion : IEntityTypeConfiguration<TipoMaterial>
    {
        public void Configure(EntityTypeBuilder<TipoMaterial> builder)
        {
            builder.ToTable("TiposMateriales");

            builder.HasKey(e => e.IdTipoMaterial);

            builder.Property(e => e.NombreMaterial)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}