using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.API.Modulos.Repositorio.Dominio.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Servicios
{
    public class MaterialServicio : IMaterialServicio
    {
        private readonly IMaterialRepositorio _repositorio;
        private readonly IServicioAlmacenamiento _servicioAlmacenamiento;
        private readonly IComercialServicio _comercialServicio;
        private readonly IMemoryCache _cache;

        public MaterialServicio(
            IMaterialRepositorio repositorio,
            IServicioAlmacenamiento servicioAlmacenamiento,
            IComercialServicio comercialServicio,
            IMemoryCache cache)
        {
            _repositorio = repositorio;
            _servicioAlmacenamiento = servicioAlmacenamiento;
            _comercialServicio = comercialServicio;
            _cache = cache;
        }

        public async Task<bool> ModerarMaterialAsync(int idMaterial, EstadoMaterial nuevoEstado, int idModerador)
        {
            var material = await _repositorio.ObtenerPorIdAsync(idMaterial);

            if (material == null)
            {
                return false;
            }

            material.Estado = nuevoEstado;
            material.FechaModeracion = DateTime.UtcNow;
            material.IdModerador = idModerador;

            await _repositorio.ActualizarAsync(material);

            _cache.Remove($"materiales_aprobados_materia_{material.IdMateria}");

            return true;
        }

        public Task<bool> ModerarMaterialAsync(int idMaterial, string nuevoEstado, int idModerador)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MaterialRespuestaDto>> ObtenerMaterialesAprobadosPorMateriaAsync(int idMateria)
        {
            string llaveCache = $"materiales_aprobados_materia_{idMateria}";

            if (_cache.TryGetValue(llaveCache, out IEnumerable<MaterialRespuestaDto>? materialesEnCache))
            {
                return materialesEnCache!;
            }

            var materiales = await _repositorio.ObtenerAprobadosPorMateriaAsync(idMateria);

            var resultado = materiales.Select(m => new MaterialRespuestaDto
            {
                IdMaterial = m.IdMaterial,
                Titulo = m.Titulo,
                TamanoBytes = m.TamanoBytes,
                IdTipoMaterial = m.IdTipoMaterial
            }).ToList();

            var opcionesCache = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            };

            _cache.Set(llaveCache, resultado, opcionesCache);

            return resultado;
        }

        public async Task<string> ObtenerUrlDescargaAsync(int idMaterial)
        {
            var material = await _repositorio.ObtenerPorIdAsync(idMaterial);

            if (material == null || material.Estado != EstadoMaterial.Aprobado)
            {
                return string.Empty;
            }

            return _servicioAlmacenamiento.GenerarUrlPreFirmada(material.UrlArchivo, 15);
        }

        public async Task<int> SubirMaterialAsync(SubirMaterialDto peticion, int idUsuarioLogueado, string rolUsuario)
        {
            int idInstitucion = await _repositorio.ObtenerIdInstitucionPorMateriaAsync(peticion.IdMateria);

            if (idInstitucion == 0)
            {
                throw new ArgumentException("La materia especificada no existe o no pertenece a una institución válida.");
            }

            var estadoSuscripcion = await _comercialServicio.ConsultarEstadoSuscripcionAsync(idInstitucion);

            if (!estadoSuscripcion.TieneSuscripcionActiva)
            {
                throw new InvalidOperationException("La institución no cuenta con una suscripción activa.");
            }

            long almacenamientoUsado = await _repositorio.CalcularAlmacenamientoUsadoAsync(idInstitucion);
            long tamanoNuevoArchivo = peticion.Archivo.Length;
            long almacenamientoProyectado = almacenamientoUsado + tamanoNuevoArchivo;

            long limiteBytes = estadoSuscripcion.LimiteAlmacenamientoGb * 1073741824L;

            if (almacenamientoProyectado > limiteBytes)
            {
                throw new InvalidOperationException("Se ha superado el límite de almacenamiento del plan contratado.");
            }

            bool esSubidaOficial = rolUsuario == "Institucion" || rolUsuario == "Profesor" || rolUsuario == "Administrador";
            EstadoMaterial estadoInicial = esSubidaOficial ? EstadoMaterial.Aprobado : EstadoMaterial.Pendiente;
            DateTime? fechaModeracion = esSubidaOficial ? DateTime.UtcNow : null;

            using var flujoArchivo = peticion.Archivo.OpenReadStream();
            string llaveObjetoGenerada = await _servicioAlmacenamiento.SubirArchivoAsync(
                flujoArchivo,
                peticion.Archivo.FileName,
                peticion.Archivo.ContentType
            );

            var nuevoMaterial = new Material
            {
                Titulo = peticion.Titulo,
                UrlArchivo = llaveObjetoGenerada,
                TamanoBytes = (int)tamanoNuevoArchivo,
                IdMateria = peticion.IdMateria,
                IdTipoMaterial = peticion.IdTipoMaterial,
                IdUsuario = idUsuarioLogueado,
                Estado = estadoInicial,
                FechaModeracion = fechaModeracion
            };

            await _repositorio.AgregarAsync(nuevoMaterial);

            if (estadoInicial == EstadoMaterial.Aprobado)
            {
                _cache.Remove($"materiales_aprobados_materia_{peticion.IdMateria}");
            }

            return nuevoMaterial.IdMaterial;
        }
    }
}