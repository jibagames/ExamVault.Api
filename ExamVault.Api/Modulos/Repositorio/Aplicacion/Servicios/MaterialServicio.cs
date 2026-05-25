using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Servicios
{
    public class MaterialServicio : IMaterialServicio
    {
        private readonly IMaterialRepositorio _repositorio;
        private readonly IServicioAlmacenamiento _servicioAlmacenamiento;

        public MaterialServicio(IMaterialRepositorio repositorio, IServicioAlmacenamiento servicioAlmacenamiento)
        {
            _repositorio = repositorio;
            _servicioAlmacenamiento = servicioAlmacenamiento;
        }

        public async Task<int> SubirMaterialAsync(SubirMaterialDto peticion, int idUsuarioLogueado, string rolUsuario)
        {
            bool esSubidaOficial = rolUsuario == "Institucion" || rolUsuario == "Profesor" || rolUsuario == "Administrador";
            string estadoInicial = esSubidaOficial ? "APROBADO" : "PENDIENTE";
            DateTime? fechaModeracion = esSubidaOficial ? DateTime.UtcNow : null;

            var nuevoMaterial = new Material
            {
                Titulo = peticion.Titulo,
                UrlArchivo = peticion.UrlArchivoR2,
                TamanoBytes = peticion.TamanoBytes,
                IdMateria = peticion.IdMateria,
                IdTipoMaterial = peticion.IdTipoMaterial,
                IdUsuario = idUsuarioLogueado,
                Estado = estadoInicial,
                FechaModeracion = fechaModeracion
            };

            await _repositorio.AgregarAsync(nuevoMaterial);
            return nuevoMaterial.IdMaterial;
        }

        public async Task<bool> ModerarMaterialAsync(int idMaterial, string nuevoEstado, int idModerador)
        {
            var material = await _repositorio.ObtenerPorIdAsync(idMaterial);

            if (material == null || material.Estado != "PENDIENTE")
            {
                return false;
            }

            material.Estado = nuevoEstado;
            material.IdModerador = idModerador;
            material.FechaModeracion = DateTime.UtcNow;

            await _repositorio.ActualizarAsync(material);
            return true;
        }

        public async Task<IEnumerable<MaterialRespuestaDto>> ObtenerMaterialesAprobadosPorMateriaAsync(int idMateria)
        {
            var materiales = await _repositorio.ObtenerAprobadosPorMateriaAsync(idMateria);

            return materiales.Select(m => new MaterialRespuestaDto
            {
                IdMaterial = m.IdMaterial,
                Titulo = m.Titulo,
                TamanoBytes = m.TamanoBytes,
                IdTipoMaterial = m.IdTipoMaterial
            });
        }

        public async Task<string> ObtenerUrlDescargaAsync(int idMaterial)
        {
            var material = await _repositorio.ObtenerPorIdAsync(idMaterial);

            if (material == null || material.Estado != "APROBADO")
            {
                return string.Empty;
            }

            return _servicioAlmacenamiento.GenerarUrlPreFirmada(material.UrlArchivo, 15);
        }
    }
}