using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Configuraciones
{
    public class DescargaConfiguracion : IEntityTypeConfiguration<Descarga>
    {
        public void Configure(EntityTypeBuilder<Descarga> builder)
        {
            builder.ToTable("Descargas");

            builder.HasKey(e => e.IdDescarga);

            builder.Property(e => e.FechaDescarga)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Material>()
                .WithMany()
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}