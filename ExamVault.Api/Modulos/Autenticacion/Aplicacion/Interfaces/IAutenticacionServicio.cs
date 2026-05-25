using ExamVault.Api.Modulos.Autenticacion.Aplicacion.DTOs;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task RegistrarAsync(RegistroDto peticion);
        Task<RespuestaInicioSesionDto> IniciarSesionAsync(IniciarSesionDto peticion);
        Task ActualizarPerfilAsync(int idUsuario, ActualizarPerfilDto peticion);
        Task<IEnumerable<dynamic>> ObtenerInstitucionesActivasAsync();
        Task AsignarRolAsync(AsignarRolDto peticion);
    }
}