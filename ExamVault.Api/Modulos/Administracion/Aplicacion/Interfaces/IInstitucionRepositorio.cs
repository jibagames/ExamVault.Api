using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IInstitucionRepositorio
    {
        Task<bool> ExisteDominioAsync(string dominioCorreo);
        Task<Institucion> CrearAsync(Institucion institucion);
        Task<IEnumerable<Institucion>> ObtenerTodasAsync();
    }
}