using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces
{
    public interface IInstitucionRepositorio
    {
        Task<Institucion?> ObtenerPorDominioAsync(string dominio);
        Task<IEnumerable<dynamic>> ObtenerActivasAsync();
    }
}