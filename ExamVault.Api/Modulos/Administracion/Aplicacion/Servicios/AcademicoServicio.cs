using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Servicios
{
    public class AcademicoServicio : IAcademicoServicio
    {
        private readonly IAcademicoRepositorio _repositorio;

        public AcademicoServicio(IAcademicoRepositorio repositorio) => _repositorio = repositorio;

        public async Task<Programa> RegistrarProgramaAsync(CrearProgramaDto peticion)
        {
            var programa = new Programa
            {
                NombrePrograma = peticion.NombrePrograma,
                Descripcion = peticion.Descripcion,
                IdInstituciones = peticion.IdInstituciones
            };
            return await _repositorio.CrearProgramaAsync(programa);
        }

        public async Task<Materia> RegistrarMateriaAsync(CrearMateriaDto peticion)
        {
            var materia = new Materia
            {
                Nombre = peticion.Nombre,
                Codigo = peticion.Codigo,
                IdInstituciones = peticion.IdInstituciones
            };
            return await _repositorio.CrearMateriaAsync(materia);
        }

        public async Task AsignarMateriaAsync(AsignarMateriaProgramaDto peticion)
        {
            if (!await _repositorio.ExisteProgramaAsync(peticion.IdPrograma))
                throw new ArgumentException("El programa no existe.");

            if (!await _repositorio.ExisteMateriaAsync(peticion.IdMateria))
                throw new ArgumentException("La materia no existe.");

            if (await _repositorio.ExisteAsignacionAsync(peticion.IdPrograma, peticion.IdMateria))
                throw new ArgumentException("La materia ya está asignada a este programa.");

            var asignacion = new ProgramaMateria
            {
                IdPrograma = peticion.IdPrograma,
                IdMateria = peticion.IdMateria
            };

            await _repositorio.AsignarMateriaAProgramaAsync(asignacion);
        }
    }
}