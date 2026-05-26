using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Administracion.Presentacion.Controladores
{
    [ApiController]
    [Route("api/administracion/comercial")]
    [Authorize(Roles = "Administrador")]
    public class ComercialControlador : ControllerBase
    {
        private readonly IComercialServicio _servicio;

        public ComercialControlador(IComercialServicio servicio) => _servicio = servicio;

        [HttpPost("planes")]
        public async Task<IActionResult> CrearPlan([FromBody] CrearPlanDto peticion)
        {
            var plan = await _servicio.RegistrarPlanAsync(peticion);
            return Ok(plan);
        }

        [HttpPost("suscripciones")]
        public async Task<IActionResult> ActivarSuscripcion([FromBody] CrearSuscripcionDto peticion)
        {
            try
            {
                var suscripcion = await _servicio.ActivarSuscripcionAsync(peticion);
                return Ok(suscripcion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}