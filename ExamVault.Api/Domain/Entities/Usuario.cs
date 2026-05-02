namespace ExamVault.Api.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string PrimerNombre { get; set; } = string.Empty;
        public string? SegundoNombre { get; set; }
        public string Apellidos { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string? Contacto { get; set; }
        public int IdInstituciones { get; set; }
    }
}