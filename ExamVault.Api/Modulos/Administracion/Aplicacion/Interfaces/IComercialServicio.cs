using ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs;
using ExamVault.Api.Modulos.Administracion.Dominio.Entidades;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces
{
    public interface IComercialServicio
    {
        Task<Plan> RegistrarPlanAsync(CrearPlanDto peticion);
        Task<Suscripcion> ActivarSuscripcionAsync(CrearSuscripcionDto peticion);
        Task<EstadoSuscripcionDto> ConsultarEstadoSuscripcionAsync(int idInstitucion);
    }
}