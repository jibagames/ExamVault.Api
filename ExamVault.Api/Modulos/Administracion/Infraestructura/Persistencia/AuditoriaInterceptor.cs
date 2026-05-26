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
                var auditoria = new Auditoria
                {
                    FechaAccion = DateTime.UtcNow,
                    AccionAuditada = entrada.State.ToString(),
                    Detalle = $"Entidad: {entrada.Entity.GetType().Name}, ID: {entrada.OriginalValues["Id" + entrada.Entity.GetType().Name] ?? "Nuevo"}",
                    IpOrigen = ip,
                    IdUsuario = usuarioId
                };

                await dbContext.Set<Auditoria>().AddAsync(auditoria, cancellationToken);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private int ObtenerUsuarioId()
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : 0; 
        }
    }
}