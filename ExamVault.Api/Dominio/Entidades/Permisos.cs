namespace ExamVault.Api.Dominio.Entidades
{
    public class Permisos
    {
        public int IdPermiso { get; set; }
        public int IdRol { get; set; }
        public string NombreTabla { get; set; } = string.Empty;
        public string Permiso { get; set; } = string.Empty;
    }
}
