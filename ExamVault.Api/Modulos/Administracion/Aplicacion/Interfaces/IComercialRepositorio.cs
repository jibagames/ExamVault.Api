using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IComercialRepositorio
    {
        Task<Plan> CrearPlanAsync(Plan plan);
        Task<Suscripcion> CrearSuscripcionAsync(Suscripcion suscripcion);
        Task<Plan?> ObtenerPlanAsync(int idPlan);
        Task<bool> TieneSuscripcionActivaAsync(int idInstitucion);
        Task<(bool EsActiva, int LimiteGb)> ObtenerEstadoSuscripcionConLimiteAsync(int idInstitucion);
    }
}