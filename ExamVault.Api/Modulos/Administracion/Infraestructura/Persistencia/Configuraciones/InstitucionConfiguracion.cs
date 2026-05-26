using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Configuraciones
{
    public class InstitucionConfiguracion : IEntityTypeConfiguration<Institucion>
    {
        public void Configure(EntityTypeBuilder<Institucion> builder)
        {
            builder.ToTable("Instituciones");
            builder.HasKey(i => i.IdInstituciones);

            builder.Property(i => i.NombreInstitucion).IsRequired().HasMaxLength(200);
            builder.Property(i => i.DominioCorreo).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Estado).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Creado).HasColumnType("timestamp with time zone");

            // Índice único para evitar dominios duplicados a nivel de base de datos
            builder.HasIndex(i => i.DominioCorreo).IsUnique();
        }
    }
}