namespace ExamVault.Api.Domain.Entities
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int IdInstituciones { get; set; }
    }
}
