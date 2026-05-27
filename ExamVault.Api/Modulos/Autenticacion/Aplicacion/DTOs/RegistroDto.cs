using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Autenticacion.Aplicacion.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El primer nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre es muy largo.")]
        public string PrimerNombre { get; set; } = string.Empty;

        [MaxLength(100)]
        public string SegundoNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [MaxLength(255, ErrorMessage = "El correo no puede superar los 255 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contrasena { get; set; } = string.Empty;
    }
}