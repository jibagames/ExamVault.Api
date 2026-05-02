namespace ExamVault.Api.Domain.Entities
{
    public class Descarga
    {
        public int IdDescarga { get; set; }
        public DateTime FechaDescarga { get; set; }
        public int IdUsuario { get; set; }
        public int IdMaterial { get; set; }
    }
}