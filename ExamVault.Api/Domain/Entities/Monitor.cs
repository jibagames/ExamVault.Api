namespace ExamVault.Api.Domain.Entities
{
    public class Monitor
    {
        public int IdMonitor { get; set; }

        public int IdUsuario { get; set; }
        public string Disponibilidad { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
    }
}
