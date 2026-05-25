namespace ExamVault.Api.Modulos.Administracion.Dominio.Entidades
{
    public class Auditoria
    {
        public int IdAuditoria { get; set; }
        public DateTime FechaAccion { get; set; }
        public string AccionAuditada { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string IpOrigen { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
    }
}

