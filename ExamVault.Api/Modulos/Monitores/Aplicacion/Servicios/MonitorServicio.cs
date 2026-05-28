using ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Monitores.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;
using ExamVault.API.Modulos.Monitores.Dominio.Enums; 

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.Servicios
{
    public class MonitorServicio : IMonitorServicio
    {
        private readonly IMonitorRepositorio _repositorio;

        public MonitorServicio(IMonitorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Dominio.Entidades.Monitor> RegistrarMonitorAsync(RegistrarMonitorDto peticion, int idUsuario)
        {
            if (await _repositorio.ExisteMonitorAsync(idUsuario))
            {
                throw new InvalidOperationException("El usuario ya está registrado como monitor.");
            }

            var monitor = new Dominio.Entidades.Monitor
            {
                IdUsuario = idUsuario,
                Disponibilidad = peticion.Disponibilidad,
                Presentacion = peticion.Presentacion
            };

            return await _repositorio.CrearMonitorAsync(monitor);
        }

        public async Task AsignarMateriaAMonitorAsync(int idMonitor, int idMateria)
        {
            var asignacion = new MonitorMateria
            {
                IdMonitor = idMonitor,
                IdMateria = idMateria
            };

            await _repositorio.AsignarMateriaAsync(asignacion);
        }

        public async Task<SesionMonitoria> SolicitarSesionAsync(SolicitarSesionDto peticion, int idUsuarioEstudiante)
        {
            if (peticion.FechaProgramada <= DateTime.UtcNow)
            {
                throw new ArgumentException("La fecha programada debe ser en el futuro.");
            }

            var sesion = new SesionMonitoria
            {
                IdMonitor = peticion.IdMonitor,
                IdUsuario = idUsuarioEstudiante,
                FechaProgramada = peticion.FechaProgramada,
                Modalidad = peticion.Modalidad,
                Ubicacion = peticion.Ubicacion,
                Estado = EstadoSesion.Pendiente,
                SolicitadoEn = DateTime.UtcNow
            };

            return await _repositorio.CrearSesionAsync(sesion);
        }

        public async Task ResponderSolicitudSesionAsync(int idSesion, EstadoSesion nuevoEstado, int idUsuarioMonitor)
        {
            var sesion = await _repositorio.ObtenerSesionAsync(idSesion);

            if (sesion == null)
            {
                throw new ArgumentException("La sesión no existe.");
            }

            sesion.Estado = nuevoEstado; 
            await _repositorio.ActualizarSesionAsync(sesion);
        }

        public async Task<Calificacion> CalificarSesionAsync(CalificarSesionDto peticion, int idUsuarioEstudiante)
        {
            var sesion = await _repositorio.ObtenerSesionAsync(peticion.IdSesion);

            if (sesion == null || sesion.IdUsuario != idUsuarioEstudiante)
            {
                throw new ArgumentException("La sesión no existe o no pertenece al estudiante.");
            }

            if (sesion.Estado != EstadoSesion.Completa)
            {
                throw new InvalidOperationException("Solo se pueden calificar sesiones completadas.");
            }

            var calificacion = new Calificacion
            {
                IdSesion = peticion.IdSesion,
                Estrellas = peticion.Estrellas,
                Comentario = peticion.Comentario,
                CreadoEn = DateTime.UtcNow
            };

            return await _repositorio.CrearCalificacionAsync(calificacion);
        }


        public async Task<IEnumerable<Dominio.Entidades.Monitor>> ListarMonitoresPorMateriaAsync(int idMateria)
        {
            return await _repositorio.ObtenerMonitoresPorMateriaAsync(idMateria);
        }

        public async Task<IEnumerable<SesionMonitoria>> ListarMisSesionesAsync(int idUsuario, string rol)
        {
            return await _repositorio.ObtenerSesionesPorUsuarioAsync(idUsuario, rol);
        }

        public Task ResponderSolicitudSesionAsync(int idSesion, string nuevoEstado, int idUsuarioMonitor)
        {
            throw new NotImplementedException();
        }
    }
}