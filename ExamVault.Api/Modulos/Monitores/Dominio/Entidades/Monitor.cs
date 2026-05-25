namespace ExamVault.Api.Modulos.Monitores.Dominio.Entidades
{
    public class Monitor
    {
        public int IdMonitor { get; set; }

        public int IdUsuario { get; set; }
        public string Disponibilidad { get; set; } = string.Empty;
        public string Presentacion { get; set; } = string.Empty;
    }
}