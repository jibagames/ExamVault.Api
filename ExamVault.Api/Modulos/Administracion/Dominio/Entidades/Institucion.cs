namespace ExamVault.Api.Modulos.Administracion.Dominio.Entidades
{
    public class Institucion
    {
        public int IdInstituciones { get; set; }
        public string NombreInstitucion { get; set; } = string.Empty;
        public string DominioCorreo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime Creado { get; set; }
    }
}
