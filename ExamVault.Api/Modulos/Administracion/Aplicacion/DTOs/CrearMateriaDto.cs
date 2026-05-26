using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class CrearMateriaDto
    {
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        public int IdInstituciones { get; set; }
    }
}
