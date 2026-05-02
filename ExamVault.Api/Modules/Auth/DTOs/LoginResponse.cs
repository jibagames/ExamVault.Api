namespace ExamVault.Api.Modules.Auth.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioInfo Usuario { get; set; } = new UsuarioInfo();
    }

    public class UsuarioInfo
    {
        public int Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int InstitucionId { get; set; }
    }
}
