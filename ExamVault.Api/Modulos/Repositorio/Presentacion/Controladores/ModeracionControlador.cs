using System.Security.Claims;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Repositorio.Presentacion.Controladores
{
    [ApiController]
    [Route("api/repositorio/moderacion")]
    [Authorize(Roles = "Administrador")]
    public class ModeracionControlador : ControllerBase
    {
        private readonly IMaterialServicio _materialServicio;

        public ModeracionControlador(IMaterialServicio materialServicio)
        {
            _materialServicio = materialServicio;
        }

        [HttpPatch("{idMaterial}/estado")]
        public async Task<IActionResult> CambiarEstadoMaterial(int idMaterial, [FromBody] ModerarMaterialDto peticion)
        {
            var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claimId) || !int.TryParse(claimId, out int idModerador))
            {
                return Unauthorized(new { mensaje = "Token inválido." });
            }

            var resultado = await _materialServicio.ModerarMaterialAsync(idMaterial, peticion.NuevoEstado, idModerador);

            if (!resultado)
            {
                return NotFound(new { mensaje = "Material no encontrado o ya fue moderado." });
            }

            return Ok(new { mensaje = $"Material actualizado a estado {peticion.NuevoEstado} exitosamente." });
        }
    }
}