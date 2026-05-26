using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IAcademicoServicio
    {
        Task<Programa> RegistrarProgramaAsync(CrearProgramaDto peticion);
        Task<Materia> RegistrarMateriaAsync(CrearMateriaDto peticion);
        Task AsignarMateriaAsync(AsignarMateriaProgramaDto peticion);
    }
}