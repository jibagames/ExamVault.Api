using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs
{
    public class SolicitarSesionDto
    {
        [Required]
        public int IdMonitor { get; set; }

        [Required]
        public DateTime FechaProgramada { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modalidad { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Ubicacion { get; set; }
    }
}