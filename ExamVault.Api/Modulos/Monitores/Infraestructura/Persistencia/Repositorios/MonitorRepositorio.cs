using Microsoft.EntityFrameworkCore;
using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Monitores.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Monitores.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Monitores.Infraestructura.Persistencia.Repositorios
{
    public class MonitorRepositorio : IMonitorRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public MonitorRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Dominio.Entidades.Monitor> CrearMonitorAsync(Dominio.Entidades.Monitor monitor)
        {
            await _contexto.Set<Dominio.Entidades.Monitor>().AddAsync(monitor);
            await _contexto.SaveChangesAsync();
            return monitor;
        }

        public async Task AsignarMateriaAsync(MonitorMateria asignacion)
        {
            await _contexto.Set<MonitorMateria>().AddAsync(asignacion);
            await _contexto.SaveChangesAsync();
        }

        public async Task<SesionMonitoria> CrearSesionAsync(SesionMonitoria sesion)
        {
            await _contexto.Set<SesionMonitoria>().AddAsync(sesion);
            await _contexto.SaveChangesAsync();
            return sesion;
        }

        public async Task ActualizarSesionAsync(SesionMonitoria sesion)
        {
            _contexto.Set<SesionMonitoria>().Update(sesion);
            await _contexto.SaveChangesAsync();
        }

        public async Task<SesionMonitoria?> ObtenerSesionAsync(int idSesion)
        {
            return await _contexto.Set<SesionMonitoria>().FirstOrDefaultAsync(s => s.IdSesion == idSesion);
        }

        public async Task<Calificacion> CrearCalificacionAsync(Calificacion calificacion)
        {
            await _contexto.Set<Calificacion>().AddAsync(calificacion);
            await _contexto.SaveChangesAsync();
            return calificacion;
        }

        public async Task<bool> ExisteMonitorAsync(int idUsuario)
        {
            return await _contexto.Set<Dominio.Entidades.Monitor>().AnyAsync(m => m.IdUsuario == idUsuario);
        }

        public async Task<IEnumerable<Dominio.Entidades.Monitor>> ObtenerMonitoresPorMateriaAsync(int idMateria)
        {
            var idsMonitores = await _contexto.Set<MonitorMateria>()
                .Where(mm => mm.IdMateria == idMateria)
                .Select(mm => mm.IdMonitor)
                .ToListAsync();

            return await _contexto.Set<Dominio.Entidades.Monitor>()
                .Where(m => idsMonitores.Contains(m.IdMonitor))
                .ToListAsync();
        }

        public async Task<Dominio.Entidades.Monitor?> ObtenerMonitorPorUsuarioAsync(int idUsuario)
        {
            return await _contexto.Set<Dominio.Entidades.Monitor>().FirstOrDefaultAsync(m => m.IdUsuario == idUsuario);
        }

        public async Task<IEnumerable<SesionMonitoria>> ObtenerSesionesPorUsuarioAsync(int idUsuario, string rol)
        {
            if (rol.ToUpper() == "MONITOR")
            {
                var monitor = await ObtenerMonitorPorUsuarioAsync(idUsuario);
                if (monitor == null)
                {
                    return new List<SesionMonitoria>();
                }
                return await _contexto.Set<SesionMonitoria>()
                    .Where(s => s.IdMonitor == monitor.IdMonitor)
                    .ToListAsync();
            }

            return await _contexto.Set<SesionMonitoria>()
                .Where(s => s.IdUsuario == idUsuario)
                .ToListAsync();
        }
    }
}