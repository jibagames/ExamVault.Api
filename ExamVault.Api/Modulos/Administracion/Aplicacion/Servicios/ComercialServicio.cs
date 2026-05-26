using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Servicios
{
    public class ComercialServicio : IComercialServicio
    {
        private readonly IComercialRepositorio _repositorio;

        public ComercialServicio(IComercialRepositorio repositorio) => _repositorio = repositorio;

        public async Task<Plan> RegistrarPlanAsync(CrearPlanDto peticion)
        {
            var plan = new Plan
            {
                NombreDelPlan = peticion.NombreDelPlan,
                LimiteAlmacenamiento = peticion.LimiteAlmacenamiento,
                PrecioMensual = peticion.PrecioMensual,
                LimiteEstudiantes = peticion.LimiteEstudiantes
            };
            return await _repositorio.CrearPlanAsync(plan);
        }

        public async Task<Suscripcion> ActivarSuscripcionAsync(CrearSuscripcionDto peticion)
        {
            if (await _repositorio.TieneSuscripcionActivaAsync(peticion.IdInstituciones))
                throw new ArgumentException("La institución ya tiene una suscripción activa.");

            var plan = await _repositorio.ObtenerPlanAsync(peticion.IdPlanes);
            if (plan == null)
                throw new ArgumentException("El plan seleccionado no existe.");

            var suscripcion = new Suscripcion
            {
                IdPlanes = peticion.IdPlanes,
                IdInstituciones = peticion.IdInstituciones,
                FechaInicio = DateTime.UtcNow,
                FechaFin = DateTime.UtcNow.AddMonths(peticion.MesesDuracion),
                Estado = "ACTIVA"
            };

            return await _repositorio.CrearSuscripcionAsync(suscripcion);
        }

        public async Task<EstadoSuscripcionDto> ConsultarEstadoSuscripcionAsync(int idInstitucion)
        {
            var (esActiva, limiteGb) = await _repositorio.ObtenerEstadoSuscripcionConLimiteAsync(idInstitucion);

            return new EstadoSuscripcionDto
            {
                TieneSuscripcionActiva = esActiva,
                LimiteAlmacenamientoGb = limiteGb
            };
        }
    }
}