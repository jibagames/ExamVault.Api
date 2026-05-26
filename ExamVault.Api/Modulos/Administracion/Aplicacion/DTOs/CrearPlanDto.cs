using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class CrearPlanDto
    {
        [Required]
        [MaxLength(100)]
        public string NombreDelPlan { get; set; } = string.Empty;

        [Required]
        public int LimiteAlmacenamiento { get; set; }

        [Required]
        public decimal PrecioMensual { get; set; }

        [Required]
        public int LimiteEstudiantes { get; set; }
    }
}