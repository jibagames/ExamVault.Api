using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IAdministracionServicio
    {
        Task<Institucion> RegistrarInstitucionAsync(CrearInstitucionDto peticion);
        Task<IEnumerable<Institucion>> ListarInstitucionesAsync();
    }
}