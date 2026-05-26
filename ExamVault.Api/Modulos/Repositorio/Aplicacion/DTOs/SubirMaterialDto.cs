using System.ComponentModel.DataAnnotations;

namespace ExamVault.Api.Modulos.Repositorio.Aplicacion.DTOs
{
    public class SubirMaterialDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El título no puede superar los 255 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El archivo físico es obligatorio.")]
        public IFormFile Archivo { get; set; } = null!;

        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El ID del tipo de material es obligatorio.")]
        public int IdTipoMaterial { get; set; }
    }
}