using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Monitores.Aplicacion.DTOs
{
    public class CalificarSesionDto
    {
        [Required]
        public int IdSesion { get; set; }

        [Required]
        [Range(1, 5)]
        public int Estrellas { get; set; }

        [MaxLength(500)]
        public string? Comentario { get; set; }
    }
}
