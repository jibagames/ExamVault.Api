using ExamVault.API.Modulos.Monitores.Dominio.Enums;

namespace ExamVault.Api.Modulos.Monitores.Dominio.Entidades
{
    public class SesionMonitoria
    {
        public int IdSesion { get; set; }
        public EstadoSesion Estado { get; set; } = EstadoSesion.Pendiente;
        public DateTime SolicitadoEn { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Modalidad { get; set; } = string.Empty;
        public string? Ubicacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdMonitor { get; set; }
    }
}