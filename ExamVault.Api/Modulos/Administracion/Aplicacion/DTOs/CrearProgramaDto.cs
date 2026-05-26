using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class CrearProgramaDto
    {
        [Required]
        [MaxLength(200)]
        public string NombrePrograma { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public int IdInstituciones { get; set; }
    }
}
