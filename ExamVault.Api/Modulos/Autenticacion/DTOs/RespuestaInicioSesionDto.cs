namespace ExamVault.Api.Modulos.Autenticacion.DTOs
{
    public class RespuestaInicioSesionDto
    {
        public string Token { get; set; } = string.Empty;
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    }
}
