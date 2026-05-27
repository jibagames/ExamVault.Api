using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia
{
    public class AuditoriaInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditoriaInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

            var usuarioId = ObtenerUsuarioId();
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";

            var entradas = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var entrada in entradas)
            {
                // 1. Obtenemos las propiedades que forman la llave primaria dinámicamente
                var llavePrimaria = entrada.Metadata.FindPrimaryKey();
                string idValor = "Desconocido";

                // 2. Si la entidad tiene llave(s) primaria(s), extraemos sus valores de forma segura
                if (llavePrimaria != null)
                {
                    var valores = llavePrimaria.Properties.Select(p => entrada.CurrentValues[p]?.ToString() ?? "Nulo");
                    idValor = string.Join("-", valores); // Si es compuesta, la une con un guion (ej. "1-2")
                }

                var auditoria = new Auditoria
                {
                    FechaAccion = DateTime.UtcNow,
                    AccionAuditada = entrada.State.ToString(),
                    Detalle = $"Entidad: {entrada.Entity.GetType().Name}, ID: {idValor}",
                    IpOrigen = ip,
                    IdUsuario = usuarioId
                };

                await dbContext.Set<Auditoria>().AddAsync(auditoria, cancellationToken);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private int? ObtenerUsuarioId() 
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Si hay un token válido y el ID es mayor a 0, lo retornamos
            if (!string.IsNullOrWhiteSpace(claim) && int.TryParse(claim, out var id) && id > 0)
            {
                return id;
            }

            // Si no hay token, o es inválido, retornamos null
            return null;
        }
    }
}