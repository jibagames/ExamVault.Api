using Microsoft.EntityFrameworkCore;
using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Repositorios
{
    public class ComercialRepositorio : IComercialRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public ComercialRepositorio(ApplicationDbContext contexto) => _contexto = contexto;

        public async Task<Plan> CrearPlanAsync(Plan plan)
        {
            await _contexto.Planes.AddAsync(plan);
            await _contexto.SaveChangesAsync();
            return plan;
        }

        public async Task<Suscripcion> CrearSuscripcionAsync(Suscripcion suscripcion)
        {
            await _contexto.Suscripciones.AddAsync(suscripcion);
            await _contexto.SaveChangesAsync();
            return suscripcion;
        }

        public async Task<Plan?> ObtenerPlanAsync(int idPlan)
        {
            return await _contexto.Planes.FirstOrDefaultAsync(p => p.IdPlanes == idPlan);
        }

        public async Task<bool> TieneSuscripcionActivaAsync(int idInstitucion)
        {
            return await _contexto.Suscripciones.AnyAsync(s =>
                s.IdInstituciones == idInstitucion &&
                s.Estado == "ACTIVA" &&
                s.FechaFin > DateTime.UtcNow);
        }

        public async Task<(bool EsActiva, int LimiteGb)> ObtenerEstadoSuscripcionConLimiteAsync(int idInstitucion)
        {
            var suscripcion = await _contexto.Suscripciones
                .Where(s => s.IdInstituciones == idInstitucion && s.Estado == "ACTIVA" && s.FechaFin > DateTime.UtcNow)
                .OrderByDescending(s => s.FechaFin)
                .FirstOrDefaultAsync();

            if (suscripcion == null)
            {
                return (false, 0);
            }

            var plan = await _contexto.Planes.FirstOrDefaultAsync(p => p.IdPlanes == suscripcion.IdPlanes);

            return (true, plan?.LimiteAlmacenamiento ?? 0);
        }
    }
}