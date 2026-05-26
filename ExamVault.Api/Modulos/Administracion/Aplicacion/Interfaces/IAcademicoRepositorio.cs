using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IAcademicoRepositorio
    {
        Task<Programa> CrearProgramaAsync(Programa programa);
        Task<Materia> CrearMateriaAsync(Materia materia);
        Task AsignarMateriaAProgramaAsync(ProgramaMateria asignacion);
        Task<bool> ExisteProgramaAsync(int idPrograma);
        Task<bool> ExisteMateriaAsync(int idMateria);
        Task<bool> ExisteAsignacionAsync(int idPrograma, int idMateria);
    }
}