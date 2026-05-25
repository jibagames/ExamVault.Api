using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces
{
    public interface IMaterialRepositorio
    {
        Task AgregarAsync(Material material);
        Task<Material?> ObtenerPorIdAsync(int idMaterial);
        Task ActualizarAsync(Material material);
        Task<IEnumerable<Material>> ObtenerAprobadosPorMateriaAsync(int idMateria);
    }
}