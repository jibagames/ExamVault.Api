using ExamVault.Api.Dominio.Entidades;
using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Repositorio.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace ExamVault.Api.Modulos.Repositorio.Controladores
{
    [Route("api/repositorio/materiales")]
    [ApiController]
    public class MaterialControlador : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public MaterialControlador(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerMaterialPorId(int id)
        {
            string llaveCache = $"material_info_{id}";

            if (!_cache.TryGetValue(llaveCache, out Material? material))
            {
                material = await _context.Materiales
                    .FirstOrDefaultAsync(m => m.IdMaterial == id && m.Estado == "APROBADO");

                if (material == null)
                {
                    return NotFound(new { mensaje = "El material solicitado no existe o no ha sido aprobado aún." });
                }

                var opcionesCache = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                _cache.Set(llaveCache, material, opcionesCache);
            }

            return Ok(material);
        }

        [HttpGet("buscar")]
        [Authorize]
        public async Task<IActionResult> BuscarMateriales([FromQuery] string filtro)
        {
            string llaveBusqueda = $"busqueda_material_{filtro.ToLower().Trim()}";

            if (!_cache.TryGetValue(llaveBusqueda, out List<Material>? resultados))
            {
                resultados = await _context.Materiales
                    .Where(m => m.Estado == "APROBADO" && m.Titulo.ToLower().Contains(filtro.ToLower()))
                    .Take(25)
                    .ToListAsync();

                var opcionesCache = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                _cache.Set(llaveBusqueda, resultados, opcionesCache);
            }

            return Ok(resultados);
        }

        [HttpPost]
        [Route("subir")]
        [Authorize(Roles = "ESTUDIANTE,MONITOR,INSTITUCIÓN,PROFESOR")]
        public async Task<IActionResult> SubirMaterial([FromBody] SubirMaterialDto request)
        {
            var idUsuarioStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rolUsuario = User.FindFirstValue(ClaimTypes.Role);
            var idUsuario = int.Parse(idUsuarioStr);

            string estadoInicial = "PENDIENTE";

            if (rolUsuario == "INSTITUCIÓN" || rolUsuario == "PROFESOR")
            {
                estadoInicial = "APROBADO";
            }

            var nuevoMaterial = new Material
            {
                Titulo = request.Titulo,
                Estado = estadoInicial,
                UrlArchivo = request.UrlArchivoR2,
                TamanoBytes = request.TamañoBytes,
                IdUsuario = idUsuario,
                IdMateria = request.IdMateria,
                IdTipoMaterial = request.IdTipoMaterial
            };

            _context.Materiales.Add(nuevoMaterial);
            await _context.SaveChangesAsync();

            if (estadoInicial == "PENDIENTE")
            {
                return CreatedAtAction(nameof(ObtenerMaterialPorId), new { id = nuevoMaterial.IdMaterial }, new { mensaje = "Material subido y en espera de aprobación.", data = nuevoMaterial });
            }

            return CreatedAtAction(nameof(ObtenerMaterialPorId), new { id = nuevoMaterial.IdMaterial }, new { mensaje = "Material publicado directamente.", data = nuevoMaterial });
        }

        [HttpDelete("cache/{id}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult InvalidarCacheMaterial(int id)
        {
            _cache.Remove($"material_info_{id}");
            return Ok(new { mensaje = "La memoria caché para este material ha sido invalidada." });
        }
    }
}