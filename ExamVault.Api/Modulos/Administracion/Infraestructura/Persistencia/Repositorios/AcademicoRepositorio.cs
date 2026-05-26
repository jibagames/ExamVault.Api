using Microsoft.EntityFrameworkCore;
using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Repositorios
{
    public class AcademicoRepositorio : IAcademicoRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public AcademicoRepositorio(ApplicationDbContext contexto) => _contexto = contexto;

        public async Task<Programa> CrearProgramaAsync(Programa programa)
        {
            await _contexto.Programas.AddAsync(programa);
            await _contexto.SaveChangesAsync();
            return programa;
        }

        public async Task<Materia> CrearMateriaAsync(Materia materia)
        {
            await _contexto.Materias.AddAsync(materia);
            await _contexto.SaveChangesAsync();
            return materia;
        }

        public async Task AsignarMateriaAProgramaAsync(ProgramaMateria asignacion)
        {
            await _contexto.ProgramasMaterias.AddAsync(asignacion);
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> ExisteProgramaAsync(int idPrograma) =>
            await _contexto.Programas.AnyAsync(p => p.IdPrograma == idPrograma);

        public async Task<bool> ExisteMateriaAsync(int idMateria) =>
            await _contexto.Materias.AnyAsync(m => m.IdMateria == idMateria);

        public async Task<bool> ExisteAsignacionAsync(int idPrograma, int idMateria) =>
            await _contexto.ProgramasMaterias.AnyAsync(pm => pm.IdPrograma == idPrograma && pm.IdMateria == idMateria);
    }
}