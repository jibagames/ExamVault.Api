using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Configuraciones
{
    public class MaterialConfiguracion : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materiales");

            builder.HasKey(e => e.IdMaterial);

            builder.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.UrlArchivo)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.FechaModeracion)
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.TamanoBytes)
                .IsRequired();

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(m => m.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(m => m.IdModerador)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<TipoMaterial>()
                .WithMany()
                .HasForeignKey(m => m.IdTipoMaterial)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}