using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces
{
    public interface IMaterialServicio
    {
        Task<int> SubirMaterialAsync(SubirMaterialDto peticion, int idUsuarioLogueado, string rolUsuario);
        Task<bool> ModerarMaterialAsync(int idMaterial, string nuevoEstado, int idModerador);
        Task<IEnumerable<MaterialRespuestaDto>> ObtenerMaterialesAprobadosPorMateriaAsync(int idMateria);
        Task<string> ObtenerUrlDescargaAsync(int idMaterial);
    }
}