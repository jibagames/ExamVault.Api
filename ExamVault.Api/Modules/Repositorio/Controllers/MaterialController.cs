using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using ExamVault.Api.Infrastructure.Data;
using ExamVault.Api.Domain.Entities;

namespace ExamVault.Api.Modules.Repositorio.Controllers
{
    [Route("api/repositorio/materiales")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public MaterialController(ApplicationDbContext context, IMemoryCache cache)
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

        [HttpDelete("cache/{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult InvalidarCacheMaterial(int id)
        {
            _cache.Remove($"material_info_{id}");
            return Ok(new { mensaje = "La memoria caché para este material ha sido invalidada." });
        }
    }
}