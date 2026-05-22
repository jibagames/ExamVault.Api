namespace ExamVault.Api.Modulos.Autenticacion.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int InstitucionId { get; set; }
    }
}