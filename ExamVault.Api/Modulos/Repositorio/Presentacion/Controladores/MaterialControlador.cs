using System.Security.Claims;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamVault.Api.Modulos.Repositorio.Presentacion.Controladores
{
    [ApiController]
    [Route("api/repositorio")]
    [Authorize]
    public class MaterialControlador : ControllerBase
    {
        private readonly IMaterialServicio _materialServicio;

        public MaterialControlador(IMaterialServicio materialServicio)
        {
            _materialServicio = materialServicio;
        }

        [HttpPost("subir")]
        [Authorize(Roles = "Estudiante,Monitor,Institucion,Profesor,Administrador")]
        public async Task<IActionResult> SubirMaterial([FromBody] SubirMaterialDto peticion)
        {
            try
            {
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var claimRol = User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(claimId) || !int.TryParse(claimId, out int idUsuario) || string.IsNullOrEmpty(claimRol))
                {
                    return Unauthorized(new { mensaje = "Token inválido. No se pudo identificar al usuario o su rol." });
                }

                var idMaterialGenerado = await _materialServicio.SubirMaterialAsync(peticion, idUsuario, claimRol);
                bool esSubidaOficial = claimRol == "Institucion" || claimRol == "Profesor" || claimRol == "Administrador";

                return Ok(new
                {
                    mensaje = esSubidaOficial
                        ? "El material fue registrado y aprobado exitosamente de forma automática."
                        : "El material fue registrado exitosamente y está pendiente de moderación.",
                    idMaterial = idMaterialGenerado,
                    estadoActual = esSubidaOficial ? "APROBADO" : "PENDIENTE"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error interno al registrar el material.", detalle = ex.Message });
            }
        }

        [HttpGet("materia/{idMateria}")]
        public async Task<IActionResult> ObtenerMateriales(int idMateria)
        {
            var materiales = await _materialServicio.ObtenerMaterialesAprobadosPorMateriaAsync(idMateria);
            return Ok(materiales);
        }

        [HttpGet("{idMaterial}/descargar")]
        public async Task<IActionResult> DescargarMaterial(int idMaterial)
        {
            var urlDescarga = await _materialServicio.ObtenerUrlDescargaAsync(idMaterial);

            if (string.IsNullOrEmpty(urlDescarga))
            {
                return NotFound(new { mensaje = "Material no encontrado, no aprobado o URL no disponible." });
            }

            return Ok(new { urlTemporal = urlDescarga });
        }
    }
}