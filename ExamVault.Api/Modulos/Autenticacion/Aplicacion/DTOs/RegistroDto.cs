namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.DTOs
{
    public class RegistroDto
    {
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}