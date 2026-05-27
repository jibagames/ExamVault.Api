using ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.Interfaces
{
    public interface IMonitorServicio
    {
        Task<Dominio.Entidades.Monitor> RegistrarMonitorAsync(RegistrarMonitorDto peticion, int idUsuario);
        Task AsignarMateriaAMonitorAsync(int idMonitor, int idMateria);
        Task<SesionMonitoria> SolicitarSesionAsync(SolicitarSesionDto peticion, int idUsuarioEstudiante);
        Task ResponderSolicitudSesionAsync(int idSesion, string nuevoEstado, int idUsuarioMonitor);
        Task<Calificacion> CalificarSesionAsync(CalificarSesionDto peticion, int idUsuarioEstudiante);
        Task<IEnumerable<Dominio.Entidades.Monitor>> ListarMonitoresPorMateriaAsync(int idMateria);
        Task<IEnumerable<SesionMonitoria>> ListarMisSesionesAsync(int idUsuario, string rol);
    }
}
