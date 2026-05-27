using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs
{
    public class RegistrarMonitorDto
    {
        [Required]
        [MaxLength(255)]
        public string Disponibilidad { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Presentacion { get; set; } = string.Empty;
    }
}