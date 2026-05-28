using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.API.Modulos.Repositorio.Dominio.Enums;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces
{
    public interface IMaterialServicio
    {
        Task<int> SubirMaterialAsync(SubirMaterialDto peticion, int idUsuarioLogueado, string rolUsuario);
        Task<bool> ModerarMaterialAsync(int idMaterial, EstadoMaterial nuevoEstado, int idModerador);
        Task<IEnumerable<MaterialRespuestaDto>> ObtenerMaterialesAprobadosPorMateriaAsync(int idMateria);
        Task<string> ObtenerUrlDescargaAsync(int idMaterial);
    }
}