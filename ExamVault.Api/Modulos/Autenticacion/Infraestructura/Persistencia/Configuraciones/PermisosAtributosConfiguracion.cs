using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones
{
    public class PermisosAtributosConfiguracion : IEntityTypeConfiguration<PermisosAtributos>
    {
        public void Configure(EntityTypeBuilder<PermisosAtributos> builder)
        {
            builder.ToTable("PermisosAtributos");

            builder.HasKey(p => p.IdPermisoAtributo);

            builder.Property(p => p.NombreTabla)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.NombreAtributo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Permiso)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne<Rol>()
                .WithMany()
                .HasForeignKey(p => p.IdRol)
                .HasConstraintName("PERMISOS_ATRIBUTOS_ROLES_FK")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}