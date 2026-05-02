namespace ExamVault.Api.Domain.Entities
{
    public class Suscripcion
    {
        public int IdSuscripciones { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int IdPlanes { get; set; }
        public int IdInstituciones { get; set; }
    }
}

