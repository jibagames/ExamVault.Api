namespace ExamVault.Api.Modulos.Autenticacion.Dominio.Entidades
{
    public class PermisosAtributos
    {
        public int IdPermisoAtributo { get; set; }
        public int IdRol { get; set; }
        public string NombreTabla { get; set; } = string.Empty;
        public string NombreAtributo { get; set; } = string.Empty;
        public string Permiso { get; set; } = string.Empty;
    }
}
