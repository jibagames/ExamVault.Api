using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Configuraciones
{
    public class UsuarioRolConfiguracion : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.ToTable("UsuariosRoles");

            builder.HasKey(ur => new { ur.IdUsuario, ur.IdRol });
        }
    }
}