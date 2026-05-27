using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.Interfaces
{
    public interface IMonitorRepositorio
    {
        Task<Dominio.Entidades.Monitor> CrearMonitorAsync(Dominio.Entidades.Monitor monitor);
        Task AsignarMateriaAsync(MonitorMateria asignacion);
        Task<SesionMonitoria> CrearSesionAsync(SesionMonitoria sesion);
        Task ActualizarSesionAsync(SesionMonitoria sesion);
        Task<SesionMonitoria?> ObtenerSesionAsync(int idSesion);
        Task<Calificacion> CrearCalificacionAsync(Calificacion calificacion);
        Task<bool> ExisteMonitorAsync(int idUsuario);
        Task<IEnumerable<Dominio.Entidades.Monitor>> ObtenerMonitoresPorMateriaAsync(int idMateria);
        Task<IEnumerable<SesionMonitoria>> ObtenerSesionesPorUsuarioAsync(int idUsuario, string rol);
        Task<Dominio.Entidades.Monitor?> ObtenerMonitorPorUsuarioAsync(int idUsuario);
    }
}