using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Administracion.Aplicacion.DTOs
{
    public class CrearSuscripcionDto
    {
        [Required]
        public int IdPlanes { get; set; }

        [Required]
        public int IdInstituciones { get; set; }

        [Required]
        public int MesesDuracion { get; set; }
    }
}
