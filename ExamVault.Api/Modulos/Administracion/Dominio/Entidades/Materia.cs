namespace ExamVault.Api.Modulos.Administracion.Dominio.Entidades
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int IdInstituciones { get; set; }
    }
}

