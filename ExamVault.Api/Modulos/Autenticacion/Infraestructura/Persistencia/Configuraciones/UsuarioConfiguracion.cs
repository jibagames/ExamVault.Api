using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones
{
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.PrimerNombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.SegundoNombre)
                .HasMaxLength(50);

            builder.Property(u => u.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Correo)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(u => u.Correo)
                .IsUnique();

            builder.Property(u => u.ContrasenaHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.FotoUrl)
                .HasMaxLength(500);

            builder.Property(u => u.Contacto)
                .HasMaxLength(50);

            builder.Property(u => u.IdInstituciones)
                .IsRequired();
        }
    }
}