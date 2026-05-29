using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Administracion.Presentacion.Controladores
{
    [ApiController]
    [Route("api/administracion/academico")]
    [Authorize(Roles = "Administrador,Institucion,Coordinador")]
    public class AcademicoControlador : ControllerBase
    {
        private readonly IAcademicoServicio _servicio;

        public AcademicoControlador(IAcademicoServicio servicio) => _servicio = servicio;

        [HttpPost("programas")]
        [Authorize(Roles = "Administrador,Institucion,Coordinador")]
        public async Task<IActionResult> CrearPrograma([FromBody] CrearProgramaDto peticion)
        {
            var programa = await _servicio.RegistrarProgramaAsync(peticion);
            return Ok(programa);
        }

        [HttpPost("materias")]
        [Authorize(Roles = "Administrador,Institucion,Coordinador")]
        public async Task<IActionResult> CrearMateria([FromBody] CrearMateriaDto peticion)
        {
            var materia = await _servicio.RegistrarMateriaAsync(peticion);
            return Ok(materia);
        }

        [HttpPost("asignaciones")]
        [Authorize(Roles = "Administrador,Institucion,Coordinador")]
        public async Task<IActionResult> AsignarMateria([FromBody] AsignarMateriaProgramaDto peticion)
        {
            try
            {
                await _servicio.AsignarMateriaAsync(peticion);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}