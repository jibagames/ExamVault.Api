using System.Security.Claims;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Autenticacion.Presentacion.Controladores
{
    [Route("api/Autenticacion")]
    [ApiController]
    public class AutenticacionControlador : ControllerBase
    {
        private readonly IAutenticacionServicio _autenticacionServicio;

        public AutenticacionControlador(IAutenticacionServicio autenticacionServicio)
        {
            _autenticacionServicio = autenticacionServicio;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDto peticion)
        {
            try
            {
                await _autenticacionServicio.RegistrarAsync(peticion);
                return Ok(new { mensaje = "Usuario registrado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionDto peticion)
        {
            try
            {
                var respuesta = await _autenticacionServicio.IniciarSesionAsync(peticion);
                return Ok(respuesta);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }

        [HttpPut("actualizar-perfil")]
        [Authorize]
        public async Task<IActionResult> ActualizarPerfil([FromBody] ActualizarPerfilDto peticion)
        {
            try
            {
                var idUsuarioReclamo = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(idUsuarioReclamo) || !int.TryParse(idUsuarioReclamo, out int idUsuario))
                {
                    return Unauthorized(new { mensaje = "Token inválido o expirado." });
                }

                await _autenticacionServicio.ActualizarPerfilAsync(idUsuario, peticion);
                return Ok(new { mensaje = "Perfil actualizado exitosamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("asignar-rol")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDto peticion)
        {
            try
            {
                await _autenticacionServicio.AsignarRolAsync(peticion);
                return Ok(new { mensaje = "Rol asignado exitosamente al usuario." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("verificar-sesion")]
        [Authorize]
        public IActionResult VerificarSesion()
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var correo = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(new
            {
                valida = true,
                mensaje = "La sesión está activa.",
                usuario = new { idUsuario, correo }
            });
        }

        [HttpGet("verificar-administrador")]
        [Authorize]
        public IActionResult VerificarAdministrador()
        {
            var esAdmin = User.IsInRole("Administrador") || User.IsInRole("Administrador");

            if (esAdmin)
            {
                return Ok(new { esAdministrador = true, mensaje = "Acceso concedido." });
            }

            return StatusCode(403, new { esAdministrador = false, mensaje = "No posee los privilegios necesarios." });
        }

        [HttpGet("instituciones-activas")]
        public async Task<IActionResult> ObtenerInstitucionesActivas()
        {
            var instituciones = await _autenticacionServicio.ObtenerInstitucionesActivasAsync();
            return Ok(instituciones);
        }

        [HttpPost("cerrar-sesion")]
        [Authorize]
        public IActionResult CerrarSesion()
        {
            return Ok(new { mensaje = "Sesión cerrada. Proceda a eliminar el token en el cliente." });
        }
    }
}