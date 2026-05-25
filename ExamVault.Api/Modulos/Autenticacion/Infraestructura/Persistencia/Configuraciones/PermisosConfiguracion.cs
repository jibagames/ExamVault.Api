using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones
{
    public class PermisosConfiguracion : IEntityTypeConfiguration<Permisos>
    {
        public void Configure(EntityTypeBuilder<Permisos> builder)
        {
            builder.ToTable("Permisos");

            builder.HasKey(p => p.IdPermiso);

            builder.Property(p => p.NombreTabla)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Permiso)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne<Rol>()
                .WithMany()
                .HasForeignKey(p => p.IdRol)
                .HasConstraintName("PERMISOS_ROLES_FK")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}