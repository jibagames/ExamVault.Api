using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Administracion.Presentacion.Controladores
{
    [ApiController]
    [Route("api/administracion/instituciones")]
    [Authorize(Roles = "Administrador, Coordinador")] 
    public class InstitucionControlador : ControllerBase
    {
        private readonly IAdministracionServicio _servicio;

        public InstitucionControlador(IAdministracionServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPost]
        public async Task<IActionResult> CrearInstitucion([FromBody] CrearInstitucionDto peticion)
        {
            try
            {
                var institucion = await _servicio.RegistrarInstitucionAsync(peticion);
                return CreatedAtAction(nameof(ObtenerInstituciones), new { id = institucion.IdInstituciones }, institucion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor.", detalle = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous] 
        public async Task<IActionResult> ObtenerInstituciones()
        {
            var instituciones = await _servicio.ListarInstitucionesAsync();
            return Ok(instituciones);
        }
    }
}