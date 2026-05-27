using System.Security.Claims;
using ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Monitores.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Monitores.Presentacion.Controladores
{
    [ApiController]
    [Route("api/monitores")]
    public class MonitorControlador : ControllerBase
    {
        private readonly IMonitorServicio _servicio;

        public MonitorControlador(IMonitorServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("registrar")]
        [Authorize(Roles = "Estudiante,Monitor")]
        public async Task<IActionResult> RegistrarMonitor([FromBody] RegistrarMonitorDto peticion)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioAutenticado();
                var monitor = await _servicio.RegistrarMonitorAsync(peticion, idUsuario);
                return Ok(monitor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("sesiones/solicitar")]
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> SolicitarSesion([FromBody] SolicitarSesionDto peticion)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioAutenticado();
                var sesion = await _servicio.SolicitarSesionAsync(peticion, idUsuario);
                return Ok(sesion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("sesiones/{idSesion}/estado")]
        [Authorize(Roles = "Monitor,Administrador")]
        public async Task<IActionResult> ResponderSesion(int idSesion, [FromBody] string estado)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioAutenticado();
                await _servicio.ResponderSolicitudSesionAsync(idSesion, estado, idUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("sesiones/calificar")]
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> CalificarSesion([FromBody] CalificarSesionDto peticion)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioAutenticado();
                var calificacion = await _servicio.CalificarSesionAsync(peticion, idUsuario);
                return Ok(calificacion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        private int ObtenerIdUsuarioAutenticado()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : throw new UnauthorizedAccessException("Usuario no válido.");
        }

        [HttpPost("asignar-materia")]
        [Authorize(Roles = "Administrador,Monitor")]
        public async Task<IActionResult> AsignarMateria([FromBody] AsignarMateriaMonitorDto peticion)
        {
            try
            {
                await _servicio.AsignarMateriaAMonitorAsync(peticion.IdMonitor, peticion.IdMateria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("materia/{idMateria}")]
        [Authorize(Roles = "Estudiante,Monitor,Administrador")]
        public async Task<IActionResult> ObtenerMonitoresPorMateria(int idMateria)
        {
            var monitores = await _servicio.ListarMonitoresPorMateriaAsync(idMateria);
            return Ok(monitores);
        }

        [HttpGet("mis-sesiones")]
        [Authorize(Roles = "Estudiante,Monitor")]
        public async Task<IActionResult> ObtenerMisSesiones()
        {
            int idUsuario = ObtenerIdUsuarioAutenticado();
            string rol = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            var sesiones = await _servicio.ListarMisSesionesAsync(idUsuario, rol);
            return Ok(sesiones);
        }
    }
}