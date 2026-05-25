namespace ExamVault.Api.Modulos.Administracion.Dominio.Entidades
{
    public class Plan
    {
        public int IdPlanes { get; set; }
        public string NombreDelPlan { get; set; } = string.Empty;
        public int LimiteAlmacenamiento { get; set; }
        public decimal PrecioMensual { get; set; }
        public int LimiteEstudiantes { get; set; }
    }
}
