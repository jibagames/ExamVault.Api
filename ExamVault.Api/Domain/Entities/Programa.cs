namespace ExamVault.Api.Domain.Entities
{
    public class Programa
    {
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int IdInstituciones { get; set; }
    }
}