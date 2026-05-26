using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Dominio.Entidades;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.Servicios
{
    public class MaterialServicio : IMaterialServicio
    {
        private readonly IMaterialRepositorio _repositorio;
        private readonly IServicioAlmacenamiento _servicioAlmacenamiento;
        private readonly IComercialServicio _comercialServicio;

        public MaterialServicio(
            IMaterialRepositorio repositorio,
            IServicioAlmacenamiento servicioAlmacenamiento,
            IComercialServicio comercialServicio)
        {
            _repositorio = repositorio;
            _servicioAlmacenamiento = servicioAlmacenamiento;
            _comercialServicio = comercialServicio;
        }

        public Task<bool> ModerarMaterialAsync(int idMaterial, string nuevoEstado, int idModerador)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MaterialRespuestaDto>> ObtenerMaterialesAprobadosPorMateriaAsync(int idMateria)
        {
            throw new NotImplementedException();
        }

        public Task<string> ObtenerUrlDescargaAsync(int idMaterial)
        {
            throw new NotImplementedException();
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
            string estadoInicial = esSubidaOficial ? "APROBADO" : "PENDIENTE";
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
            return nuevoMaterial.IdMaterial;
        }
    }
}