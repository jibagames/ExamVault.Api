using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class CrearInstitucionDto
    {
        [Required(ErrorMessage = "El nombre de la institución es obligatorio.")]
        [MaxLength(200)]
        public string NombreInstitucion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El dominio de correo es obligatorio.")]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Formato de dominio inválido (ej. universidad.edu.co)")]
        public string DominioCorreo { get; set; } = string.Empty;
    }
}