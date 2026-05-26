using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces
{
    public interface IMaterialRepositorio
    {
        Task<Material> AgregarAsync(Material material);
        Task<int> ObtenerIdInstitucionPorMateriaAsync(int idMateria);
        Task<long> CalcularAlmacenamientoUsadoAsync(int idInstitucion);
    }
}