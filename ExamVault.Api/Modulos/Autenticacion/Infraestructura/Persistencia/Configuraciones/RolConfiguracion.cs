using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones
{
    public class RolConfiguracion : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.IdRol);

            builder.Property(r => r.NombreRol)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
