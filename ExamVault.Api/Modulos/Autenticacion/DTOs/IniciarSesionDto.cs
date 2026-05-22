namespace ExamVault.Api.Modulos.Autenticacion.DTOs
{
    public class IniciarSesionDto
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}